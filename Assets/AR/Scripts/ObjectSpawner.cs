using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectManagerGameObject;  // Create a public reference to the enemy game object.
    private ObjectManager objectManager;

    [SerializeField]
    private Button startButton;

    private PlacementIndicator placementIndicator;

    [SerializeField]
    private GameObject buttonListContent;

    [SerializeField]
    private Button synthButton;

    [SerializeField]
    private Button pianoButton;

    [SerializeField]
    private Button stringsButton;

    [SerializeField]
    private Button celloButton;

    private Dictionary<string, Button> buttonReferenceDict;
    private Dictionary<string, Button> buttonDict;


    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        objectManager = objectManagerGameObject.GetComponent<ObjectManager>();


        //create a dictionary of all button references
        Debug.Log("ObjectSpawner: Start --- Creating the reference dictionary");
        buttonReferenceDict = new Dictionary<string, Button>
        {
            { "synth", synthButton },
            { "piano", pianoButton },
            { "strings", stringsButton },
            { "cello", celloButton }
        };


        buttonDict = new Dictionary<string, Button>();

        //Go through the list of instruments for the selected song
        foreach (string instrument in SongLoader.instruments)
        {
            Debug.Log("ObjectSpawner: Start --- Creating the button instance for the" + instrument);
            Debug.Log("ObjectSpawner: Start --- ---Instantiating and adding to the dictionary");
            buttonDict.Add(instrument, Instantiate(buttonReferenceDict[instrument]));
            Debug.Log("ObjectSpawner: Start --- ---Setting the parent");
            buttonDict[instrument].transform.SetParent(buttonListContent.transform);
            Debug.Log("ObjectSpawner: Start --- ---Setting active and adding functionality");

            buttonDict[instrument].gameObject.SetActive(true); //Can be removed due to the new method of instantiation

            buttonDict[instrument].onClick.AddListener(delegate { placementIndicator.SelectObject(instrument); });
            buttonDict[instrument].onClick.AddListener(delegate { prime(instrument); });
            buttonDict[instrument].gameObject.GetComponent<LongPressButton>().onLongClick.AddListener(delegate { Despawn(instrument);});
            buttonDict[instrument].gameObject.GetComponent<LongPressButton>().onLongClickRelease.AddListener(delegate { PrimeButton(instrument); });


        }

    }


    //Unselects all instruments (that are in the spawn state)
    private void ResetButtons()
    {
        foreach (KeyValuePair<string, Button> item in buttonDict)
        {   
            if(item.Value.GetComponent<Button>().enabled == true)
            {
                //Reset the graphics
                TextMeshProUGUI txt = item.Value.GetComponentInChildren<TextMeshProUGUI>();
                txt.text = "Select the " + item.Key;

                //Set all buttons to prime
                item.Value.onClick.RemoveAllListeners();
                item.Value.onClick.AddListener(delegate { placementIndicator.SelectObject(item.Key); });
                item.Value.onClick.AddListener(delegate { prime(item.Key); });
            }
        }
    }


    //Selects an instrument to be spawned
    public void prime(string key)
    {

        ResetButtons();

        //Change the text on the pressed button
        TextMeshProUGUI txt = buttonDict[key].GetComponentInChildren<TextMeshProUGUI>();
        txt.text = "Spawn the " + key;

        //Change button functionality to spawn the object
        buttonDict[key].onClick.RemoveAllListeners();
        buttonDict[key].onClick.AddListener(delegate { placementIndicator.SelectObject(key); });
        buttonDict[key].onClick.AddListener(delegate { spawn(key); });

        //Tell the placement indicator to show the indicator
        placementIndicator.ShowIndicator();

        Debug.Log("ObjectSpawner: Prime --- " + key + " primed");
    }


    //Spawns the instrument
    private void spawn(string key)
    {
        Debug.Log("ObjectSpawner: Spawn --- Spawning the " + key);

        objectManager.selectObject(key);
        bool spawned = objectManager.spawnObject(placementIndicator.transform.position, placementIndicator.transform.rotation);
        if (spawned)
        {
            Debug.Log("ObjectSpawner: Spawn --- --- Spawn successful");

            //Change the text on the button
            TextMeshProUGUI txt = buttonDict[key].GetComponentInChildren<TextMeshProUGUI>();
            txt.text = key + " spawned! Hold to reset";

            //Switch state to the despawn state - disable the button functionality and enable the long press button functionality
            buttonDict[key].GetComponent<Button>().enabled = false;
            buttonDict[key].GetComponent<LongPressButton>().enabled = true;

            //Hide the placement indicator until another object is selected (primed)
            placementIndicator.HideIndicator();
            AttemptGameStart();
        }

    }


    //Returns the button to the spawn state from the unspawn state
    //Occurs when the hold button is released aftter a successful press
    public void PrimeButton(string key)
    {
        //Switch button state
        buttonDict[key].GetComponent<LongPressButton>().enabled = false;
        buttonDict[key].GetComponent<Button>().enabled = true;

        //Prepare OnClick functionality
        buttonDict[key].onClick.RemoveAllListeners();
        buttonDict[key].onClick.AddListener(delegate { placementIndicator.SelectObject(key); });
        buttonDict[key].onClick.AddListener(delegate { prime(key); });
        //The OnClick functionality is triggered as soon as this function finishes do to the trigger still being active from the despawn button press

    }

    //Signals the ObjectManager to despawn the selected object
    //Occurs when the hold button is pressed for long enough
    public void Despawn(string key)
    {
        objectManager.selectObject(key);
        objectManager.despawnObject();

        //Reactivate the placement indicator in case all objects were placed
        placementIndicator.gameObject.SetActive(true);
        placementIndicator.SelectObject(key);
        placementIndicator.ShowIndicator();

        //Disable the Start button
        startButton.gameObject.SetActive(false);
    }

    //Attempts to move the scene to the Conducting phase if all objects have been placed
    private void AttemptGameStart()
    {
        Debug.Log("ObjectSpawner: AttemptGameStart --- Attempting to start the game");
        bool found = false;
        foreach (KeyValuePair<string, Button> item in buttonDict)
        {  
            if(buttonDict[item.Key].GetComponent<Button>().enabled == true && buttonDict[item.Key].gameObject.activeInHierarchy)
            {
                Debug.Log("ObjectSpawner: AttemptGameStart --- --- Not all objects have been placed: " + item.Key);
                found = true;
                break;
            }
        }

        if (!found)
        {
            //Deactivate the placement indicator, Activate the Start button
            Debug.Log("ObjectSpawner: AttemptGameStart --- --- All objects placed");
            placementIndicator.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
            Dictionary<string, GameObject> allObjects = objectManager.allObjectsSpawned();
            foreach(KeyValuePair<string, GameObject> entry in allObjects)
            {
                startButton.onClick.AddListener(delegate{ entry.Value.GetComponent<InstrumentControl>().Initialise(SongLoader.instrumentCues[entry.Key], SongLoader.clipNames[entry.Key]); });
            }
            
               
        }
    }




    //void Update()
    //{
    //    if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        var touchPosition = touch.position;

    //        bool isOverUI = touchPosition.IsPointOverUIObject();

    //        if (!isOverUI)
    //        {
    //            //Add here functionality for non UI touch inputs
    //        }
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectManagerGameObject;  // Create a public reference to the enemy game object.
    private ObjectManager objectManager;

    //private GameObject uiPanel;

    //[SerializeField]
    //private Button toggleButton;

    private PlacementIndicator placementIndicator;

    [SerializeField]
    private Button xylophoneButton;

    [SerializeField]
    private Button pianoButton;

    [SerializeField]
    private Button violinsButton;

    [SerializeField]
    private Button drumsButton;


    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        objectManager = objectManagerGameObject.GetComponent<ObjectManager>();

        //Go through the list of instruments for the selected song
        foreach (string instrument in SongLoader.instruments)
        {
            //Enable the correct button 
            switch (instrument)
            {
                case "xylophone":
                    xylophoneButton.gameObject.SetActive(true);
                    break;
                case "piano":
                    pianoButton.gameObject.SetActive(true);
                    break;
                case "violins":
                    violinsButton.gameObject.SetActive(true);
                    break;
                case "drums":
                    drumsButton.gameObject.SetActive(true);
                    break;
            }
        }

    }

    public void spawnCube()
    {
        objectManager.selectXylophone();
        
    }

    public void spawnSphere()
    {
        objectManager.selectPiano();
    }

    public void spawnCapsule()
    {
        objectManager.selectViolins();
    }

    public void spawnCylinder()
    {
        objectManager.selectDrums();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            var touchPosition = touch.position;

            bool isOverUI = touchPosition.IsPointOverUIObject();

            if (!isOverUI)
            {
                //Call on ObjectManager to try and create the class
                //GameObject obj = Instantiate(objectManager.objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
                objectManager.spawnObject(placementIndicator.transform.position, placementIndicator.transform.rotation);


            }
        }
    }

}

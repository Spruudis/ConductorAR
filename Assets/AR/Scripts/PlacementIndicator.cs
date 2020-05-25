using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class PlacementIndicator : MonoBehaviour
{

    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager rayManager;

    private GameObject visual;
    private Pose placementPose; //Represents the position where we can place the item
    private bool showIndicator; 


    //Store a collection of hits from Raycasting
    private List<ARRaycastHit> hits;


    private Dictionary<string, GameObject> referenceDict;

    //GameObject asset references to be instantiated
    [SerializeField]
    private GameObject xylophoneReference;

    [SerializeField]
    private GameObject pianoReference;

    [SerializeField]
    private GameObject violinsReference;

    [SerializeField]
    private GameObject drumsReference;



    void Start()
    {
        //get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        sessionOrigin = FindObjectOfType<ARSessionOrigin>(); //Find a reference to the session Origin

        //Initialise and fill the dictionary;
        referenceDict = new Dictionary<string, GameObject>
        {
            { "xylophone", xylophoneReference },
            { "piano", pianoReference },
            { "violins", violinsReference },
            { "drums", drumsReference }
        };

        //TODO: instantiate only those children which are present in the scene
        visual = transform.GetChild(0).gameObject;

        //Hide the placement visual
        visual.SetActive(false);
        showIndicator = false;

    }

    public void HideIndicator()
    {
        showIndicator = false;
        visual.SetActive(false);
    }

    public void ShowIndicator()
    {
        showIndicator = true;
    }

    void Update()
    {
        UpdatePlacementPose();

    }

    public void SelectObject (string key)
    {
        visual.SetActive(false);
        visual = referenceDict[key];
    }

    public bool IsPlaced()
    {
        return visual.active;
    }

    private void UpdatePlacementPose()
    {
        hits = new List<ARRaycastHit>();
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        rayManager.Raycast(screenCenter, hits, TrackableType.Planes);

        //if we hit an AR plane, update the position and rotation
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy && showIndicator)
            {
                visual.SetActive(true);

            }
        }

    }
}

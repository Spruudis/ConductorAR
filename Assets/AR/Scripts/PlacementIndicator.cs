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


    //Store a collection of hits from Raycasting
    private List<ARRaycastHit> hits;

    public GameObject PrefabToPlace;


    void Start()
    {
        //get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        sessionOrigin = FindObjectOfType<ARSessionOrigin>(); //Find a reference to the session Origin
        visual = transform.GetChild(0).gameObject;

        //Hide the placement visual
        visual.SetActive(false);
        //Old code
        //hits = new List<ARRaycastHit>();
    }

    void Update()
    {
        UpdatePlacementPose();

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

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
        }

    }
}

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

    void Start()
    {
        //get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        sessionOrigin = FindObjectOfType<ARSessionOrigin>(); //Find a reference to the session Origin

        //TODO: instantiate only those children which are present in the scene
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

    public void selectCube()
    {
        visual.SetActive(false);
        visual = transform.GetChild(0).gameObject;
    }

    public void selectSphere()
    {
        visual.SetActive(false);
        visual = transform.GetChild(1).gameObject;
    }

    public void selectCapsule()
    {
        visual.SetActive(false);
        visual = transform.GetChild(2).gameObject;
    }

    public void selectCylinder()
    {
        visual.SetActive(false);
        visual = transform.GetChild(3).gameObject;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    public GameObject cube;
    public GameObject sphere;
    public GameObject cylinder;
    public GameObject capsule;
    //[SerializeField]
    //private GameObject uiPanel;

    //[SerializeField]
    //private Button toggleButton;

    private PlacementIndicator placementIndicator;

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    public void spawnCube()
    {
        objectToSpawn = cube;
    }

    public void spawnSphere()
    {
        objectToSpawn = sphere;
    }

    public void spawnCapsule()
    {
        objectToSpawn = capsule;
    }

    public void spawnCylinder()
    {
        objectToSpawn = cylinder;
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
                GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            }
        }
    }

}

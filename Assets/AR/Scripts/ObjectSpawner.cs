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

    //[SerializeField]
    //private GameObject uiPanel;

    //[SerializeField]
    //private Button toggleButton;

    private PlacementIndicator placementIndicator;

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

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

    //public void Toggle()
    //{
    //    uiPanel.SetActive(!uiPanel.activeSelf);
    //    var toggleButtonText = toggleButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    //    toggleButtonText.text = uiPanel.activeSelf ? "UI OFF" : "UI ON";

    //}

}

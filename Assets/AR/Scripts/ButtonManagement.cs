using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ButtonManagement : MonoBehaviour
{
    [SerializeField]
    private Button toggleButton;

    [SerializeField]
    private GameObject uiPanel;

    public void Toggle()
    {
        uiPanel.SetActive(!uiPanel.activeSelf);
        var toggleButtonText = toggleButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        toggleButtonText.text = uiPanel.activeSelf ? "Spawn square" : "Square spawned";
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject objectManagerGameObject;
    private ObjectManager objectManager;

    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;


    public void SetResumeActive()
    {
        objectManager = objectManagerGameObject.GetComponent<ObjectManager>();
        Dictionary<string, GameObject> allObjects = objectManager.allObjectsSpawned();
        foreach(KeyValuePair<string, GameObject> entry in allObjects)
        {
            resumeButton.onClick.AddListener(delegate{ entry.Value.GetComponent<InstrumentControlAlt>().UnpauseMusic(); });
        }
    }

    public void SetRestartActive()
    {
        objectManager = objectManagerGameObject.GetComponent<ObjectManager>();
        Dictionary<string, GameObject> allObjects = objectManager.allObjectsSpawned();
        foreach(KeyValuePair<string, GameObject> entry in allObjects)
        {
            restartButton.onClick.AddListener(delegate{ entry.Value.GetComponent<InstrumentControlAlt>().RestartMusic(); });
        }
    }

    public void Quit()
    {
        objectManager = objectManagerGameObject.GetComponent<ObjectManager>();
        SceneManager.LoadScene("SongSelection");
    }
}

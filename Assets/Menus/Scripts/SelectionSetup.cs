using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SelectionSetup : MonoBehaviour
{
    private List<string> songFiles;
    public GameObject buttonPrefab;
    public GameObject ButtonListContent;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing");
        songFiles = new List<string>();
        string line = "FurElise.data";
        songFiles.Add(line);
        string path = Application.persistentDataPath + "/" + line;
        SongData data = null;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as SongData;
            stream.Close();
        }
        GameObject go = Instantiate(buttonPrefab) as GameObject;
        go.transform.SetParent(ButtonListContent.transform);
        var button = go.GetComponent<UnityEngine.UI.Button>();
        button.GetComponentInChildren<Text>().text = data.songName;
        int buttonNo = 0;
        button.onClick.AddListener(delegate { loadARScene(buttonNo); });

    }

    void loadARScene(int buttonNo) 
    {
        SongData data = null;
        Debug.Log("Number: " + buttonNo);
        string path = Application.persistentDataPath + "/" + songFiles[buttonNo];
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as SongData;
            stream.Close();
        }
        SongLoader.songName = data.songName;
        SongLoader.instruments = data.instruments;
        SceneManager.LoadScene("Orchestra");
    }

}

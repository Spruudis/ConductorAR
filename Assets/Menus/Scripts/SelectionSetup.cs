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
    public Image iconPrefab;
    public GameObject ButtonListContent;
    
    private Dictionary<string, Sprite> iconDict;

    [SerializeField]
    private Sprite synthIconReference;
    [SerializeField]
    private Sprite pianoIconReference;
    [SerializeField]
    private Sprite stringsIconReference;
    [SerializeField]
    private Sprite celloIconReference;
    [SerializeField]
    private Sprite reserveIconReference;

    void Start()
    {
        //Setup
        Debug.Log("Testing");
        songFiles = new List<string>();
        iconDict = new Dictionary<string, Sprite>()
        {
            { "synth", synthIconReference },
            { "piano", pianoIconReference },
            { "strings", stringsIconReference },
            { "cello", celloIconReference }
        };

        //Loading the song data
        try
        {
            string line;
            int i = 0;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            string txtPath = Application.persistentDataPath + "/saveFiles.txt";
            StreamReader theReader = new StreamReader(txtPath, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();
                    SongData data = null;    

                    if (line != null)
                    {
                        songFiles.Add(line);
                        string path = Application.persistentDataPath + "/" + line;
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

                        //Setting up the button text
                        button.transform.Find("TextArea/SongName").GetComponentInChildren<Text>().text = data.songName;
                        button.transform.Find("TextArea/Artist").GetComponentInChildren<Text>().text = data.author;

                        //Setting up the button icons
                        foreach (string instrument in data.instruments){
                            Image icon = Instantiate(iconPrefab) as Image;
                            icon.transform.SetParent(button.transform.Find("IconAreaViewport/IconListContent").transform);

                            if (iconDict.ContainsKey(instrument))
                            {
                                icon.sprite = iconDict[instrument];
                            }
                            else
                            {
                                icon.sprite = reserveIconReference;
                            }
                        }

                        int buttonNo = i;
                        button.onClick.AddListener(delegate{ loadARScene(buttonNo); }); //Adding button functionality
                        i++;
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
        }
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
        SongLoader.instrumentCues = loadCueDictionary(data.instruments, data.cues);
        SongLoader.clipNames = loadClipNamesDictionary(data.instruments, data.clipNames);
        SongLoader.BPM = data.BPM;
        SceneManager.LoadScene("Orchestra");
    }

    Dictionary<string, List<float>> loadCueDictionary(List<string> instruments, List<float> cues)
    {
        Dictionary<string, List<float>> instrumentCues = new Dictionary<string, List<float>>();
        int i = 0;
        int j = 0;
        List<float> instrumentSetOfCues = new List<float>();
        Debug.Log("Here is the size of cues");
        Debug.Log(cues.Count);
        while(i < cues.Count)
        {
            if(cues[i] == -1 || i == (cues.Count - 1)) {
                instrumentCues.Add(instruments[j], instrumentSetOfCues);
                instrumentSetOfCues = new List<float>();
                j++;
            } else {
                instrumentSetOfCues.Add(cues[i]);
            }
            i++;
        }
        return instrumentCues;
    }

    Dictionary<string, string> loadClipNamesDictionary(List<string> instruments, List<string> clipNames)
    {
        Dictionary<string, string> clips = new Dictionary<string, string>();
        for(var i = 0; i < instruments.Count; i++ )
        {
			clips.Add(instruments[i], clipNames[i]);
        }
        return clips;
    }

}

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
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            int i = 0;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            string txtPath = Application.streamingAssetsPath + "/saveFiles.txt";
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
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
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
                        button.GetComponentInChildren<Text>().text = data.songName;
                        int buttonNo = i;
                        button.onClick.AddListener(delegate{ loadARScene(buttonNo); });
                        i++;
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
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
        SceneManager.LoadScene("Orchestra");
    }

}

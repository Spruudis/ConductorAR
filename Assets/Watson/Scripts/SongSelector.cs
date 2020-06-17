using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using IBM.Watson.ToneAnalyzer.V3.Model;

public class SongSelector : MonoBehaviour
{

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameObject iReccomend;

    private Dictionary<string, int> emotionKey =  new Dictionary<string, int>
        {
            { "anger", 0 },
            { "fear", 1 },
            { "joy", 2 },
            { "sadness ", 3 },
            { "analytical", 4 },
            { "confident", 5 },
            { "tentative ", 6 }
        };


    public void FindMatch(List<ToneScore> input)
    {
        Debug.Log("SongSelector: Find Match --- Starting");
        try
        {
            string line;
            string bestCandidate = null;
            double bestCandidateDistance = 0;
            int i = 0;

            string txtPath = Application.persistentDataPath + "/saveFiles.txt";
            StreamReader theReader = new StreamReader(txtPath, Encoding.Default);

            using (theReader)
            {
                do
                { //For each known song
                    Debug.Log("SongSelector: Find Match --- test 1");
                    line = theReader.ReadLine();
                    if (line != null)
                    {
                        Debug.Log("SongSelector: Find Match --- test 2:" + line);
                        List<double> emotionData = null; //contains the emotion scores for the song

                        string path = Application.persistentDataPath + "/" + line.Substring(0, line.Length - 4) + "emo"; //emotion file

                        Debug.Log("SongSelector: Find Match --- test 3: Retrieve the emotion list from " + path);
                        //Retrieve the emotion list
                        if (File.Exists(path))
                        {
                            Debug.Log("SongSelector: Find Match --- test 3: the file exists");
                            BinaryFormatter formatter = new BinaryFormatter();
                            FileStream stream = new FileStream(path, FileMode.Open);

                            emotionData = formatter.Deserialize(stream) as List<double>;
                            stream.Close();
                        }

                        double distanceSquared = 0;

                        Debug.Log("SongSelector: Find Match --- test 3: Emotion list retrieved:" + emotionData[0] + ", " + emotionData[1] + ", " + emotionData[2] + ", " + emotionData[3] + ", ");

                        //Using each tone present in the response calculate the squared distance of the known song to the response
                        Debug.Log("SongSelector: Find Match --- test 4: Using each tone present in the response calculate the squared distance of the known song to the response");

                        input.ForEach(delegate (ToneScore tone)
                        {
                            Debug.Log("SongSelector: Find Match --- test 4: " + tone.ToneId);

                            if (tone.Score is double value)
                            {
                                distanceSquared += Math.Pow((value - emotionData[emotionKey[tone.ToneId]]), 2);
                            }
                        });

                        if (bestCandidate != null)
                        {
                            if(distanceSquared < bestCandidateDistance)
                            {
                                bestCandidate = line;
                                bestCandidateDistance = distanceSquared;
                            }
                        }
                        else
                        {
                            bestCandidate = line;
                            bestCandidateDistance = distanceSquared;
                        }

                    }
                } while (line != null);
                Debug.Log("SongSelector: Find Match --- test 5:");
                theReader.Close();
            }

            //Now the best song candidate has been found, proceed to create the button to start that song
            //Load the song data first
            SongData data = null;
            Debug.Log("Recomended song is " + bestCandidate);

            string songDataPath = Application.persistentDataPath + "/" + bestCandidate;
            if (File.Exists(songDataPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(songDataPath, FileMode.Open);

                data = formatter.Deserialize(stream) as SongData;
                stream.Close();
            }
            Debug.Log("SongSelector: Find Match --- test 6:");
            iReccomend.SetActive(true);
            button.SetActive(true);
            //Setting up the button text
            button.GetComponent<Button>().transform.Find("TextArea/SongName").GetComponentInChildren<Text>().text = data.songName;
            button.GetComponent<Button>().transform.Find("TextArea/Artist").GetComponentInChildren<Text>().text = data.author;
            button.GetComponent<Button>().transform.Find("TextArea/Length").GetComponentInChildren<Text>().text = data.songLengthMin + ":" + data.songLengthSec;

            ////Setting up the button icons
            //foreach (string instrument in data.instruments)
            //{
            //    Image icon = Instantiate(iconPrefab) as Image;
            //    icon.transform.SetParent(button.transform.Find("IconAreaViewport/IconListContent").transform);

            //    if (iconDict.ContainsKey(instrument))
            //    {
            //        icon.sprite = iconDict[instrument];
            //    }
            //    else
            //    {
            //        icon.sprite = reserveIconReference;
            //    }
            //}

            SongLoader.songName = data.songName;
            SongLoader.instruments = data.instruments;
            SongLoader.instrumentCues = loadCueDictionary(data.instruments, data.cues);
            SongLoader.clipNames = loadClipNamesDictionary(data.instruments, data.clipNames);
            SongLoader.BPM = data.BPM;

            button.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Orchestra"); }); //Adding button functionality
            Debug.Log("SongSelector: Find Match --- test 5:");

        }
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
        }
    }

    Dictionary<string, List<float>> loadCueDictionary(List<string> instruments, List<float> cues)
    {
        Dictionary<string, List<float>> instrumentCues = new Dictionary<string, List<float>>();
        int i = 0;
        int j = 0;
        List<float> instrumentSetOfCues = new List<float>();
        Debug.Log("Here is the size of cues");
        Debug.Log(cues.Count);
        while (i < cues.Count)
        {
            if (cues[i] == -1 || i == (cues.Count - 1))
            {
                instrumentCues.Add(instruments[j], instrumentSetOfCues);
                instrumentSetOfCues = new List<float>();
                j++;
            }
            else
            {
                instrumentSetOfCues.Add(cues[i]);
            }
            i++;
        }
        return instrumentCues;
    }

    Dictionary<string, string> loadClipNamesDictionary(List<string> instruments, List<string> clipNames)
    {
        Dictionary<string, string> clips = new Dictionary<string, string>();
        for (var i = 0; i < instruments.Count; i++)
        {
            clips.Add(instruments[i], clipNames[i]);
        }
        return clips;
    }


}

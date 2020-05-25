using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Preloader : MonoBehaviour
{

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 3.0f; //Minimum time of preloader

    void Start()
    {
        //Grabbing the only Canvasgroup in the scene 
        fadeGroup = FindObjectOfType<CanvasGroup>(); //Might break if multiple canvas group are present
        fadeGroup.alpha = 1;

        //Preload the game if anything to preload
        //$$
        if (!System.IO.File.Exists(Application.persistentDataPath + "/AJourneyToHeaven.data"))
        {
            Debug.Log("Found txt file");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/AJourneyToHeaven.data";
            FileStream stream = new FileStream(path, FileMode.Create);
            List<string> instruments = new List<string>()
            {
                "cello",
                "piano",
                "synth",
                "strings"
            };
            List<float> allCues = aJourneyToHeavenCues();
            List<string> clipNames = new List<string>()
            {
                "Cello_AJourneyToHeaven",
                "Piano_AJourneyToHeaven",
                "Synth_AJourneyToHeaven",
                "Strings_AJourneyToHeaven"
            }; 
            SongData aJourneyToHeaven = new SongData("A Journey To Heaven", instruments, allCues, clipNames);
            // Creating binary file
            formatter.Serialize(stream, aJourneyToHeaven);
            stream.Close();  
        }

        if (!System.IO.File.Exists(Application.persistentDataPath + "/saveFiles.txt")) 
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/saveFiles.txt", false);
            writer.WriteLine("AJourneyToHeaven.data");
            writer.Close();
        }
        
        //Get the timestamp of of completion time
        if(Time.time < minimumLogoTime)
        {
            loadTime = minimumLogoTime;
        }
        else
        {
            loadTime = Time.time;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Fade in
        if(Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }
        if(Time.time > minimumLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                //Load Main Menu Scene
                SceneManager.LoadScene("Menu");
            }
        }
        
    }

    List<float> aJourneyToHeavenCues()
    {
        List<float> allCues = new List<float>();
        List<float> celloCues = new List<float>(){
            33,
            72,
            78,
            172,
            181,
            217,
            264,
            308
        };
        List<float> pianoCues = new List<float>(){
            14,
            114,
            221,
            240,
            250,
            303
        };
        List<float> synthCues = new List<float>(){
            8,
            129,
            149,
            177,
            182,
            221,
            247,
            270
        };
        List<float> stringsCues = new List<float>(){
            27,
            72,
            77,
            244,
            250,
            308
        };
        allCues.AddRange(celloCues);
        allCues.Add(-1);
        allCues.AddRange(pianoCues);
        allCues.Add(-1);
        allCues.AddRange(synthCues);
        allCues.Add(-1);
        allCues.AddRange(stringsCues);
        return allCues;
    }
}

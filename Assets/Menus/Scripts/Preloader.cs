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
        Debug.Log("Preloader: Start --- Starting preloading");
        //Grabbing the only Canvasgroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;

        string songID;
        string songName;
        string songAuthor;
        int songBPM;
        int songLengthMin;
        int songLengthSec;
        List<string> songDataFilenameList = new List<string>();


        //------ Song: Journey to Heaven by Damien Deshayes ------
        //Enter song ID here
        songID = "AJourneyToHeaven"; 

        //if (!System.IO.File.Exists(Application.persistentDataPath + "/" + songID + ".data"))
        {
            Debug.Log("Preloader: Start --- Saving "+ songID +" to persistent memory");

            //------- Enter song data here ---------
            songName = "A Journey To Heaven";
            songAuthor = "Damien Deshayes";
            songBPM = 50;
            songLengthMin = 5;
            songLengthSec = 46;

            //List of instrument IDs that are present in the song
            List<string> instruments = new List<string>()
            {
                "cello",
                "piano",
                "synth",
                "strings"
            };


            //List of emotions and tones associated with the song
            //Range for each emotion should be 0 (not sad at all) to 1(definitely sad)
            List<double> emotions = new List<double>()
            {
                //anger
                0.05,
                //fear
                0.3,
                //joy
                0.5,
                //sadness
                0.6,
                //analytical
                0.1,
                //confident
                0.3,
                //tentative
                0.8
            };


            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + songID + ".data";
            FileStream stream = new FileStream(path, FileMode.Create);

            //List of the mp3 file names for each instrument
            List<string> clipNames = new List<string>()
            {
                "Cello_AJourneyToHeaven",
                "Piano_AJourneyToHeaven",
                "Synth_AJourneyToHeaven",
                "Strings_AJourneyToHeaven"
            };

            //List of the the cues for each instrument
            List<float> allCues = aJourneyToHeavenCues();

            //Creating binary file
            SongData aJourneyToHeaven = new SongData(songName, songAuthor, instruments, allCues, clipNames, songBPM, songLengthMin, songLengthSec);
            formatter.Serialize(stream, aJourneyToHeaven);
            stream.Close();

            songDataFilenameList.Add(songID + ".data");


            //save the emotions to a seperate file
            BinaryFormatter formatter2 = new BinaryFormatter();
            path = Application.persistentDataPath + "/" + songID + ".emo";
            FileStream stream2 = new FileStream(path, FileMode.Create);
            formatter2.Serialize(stream2, emotions);
            stream.Close();
        }



        //------ Song: Journey to Hell by Lucifer Morningstar ------
        //Enter song ID here
        songID = "AJourneyToHell";
        {
            Debug.Log("Preloader: Start --- Saving " + songID + " to persistent memory");

            //------- Enter song data here ---------
            songName = "A Journey To Hell";
            songAuthor = "Lucifer Morningstar";
            songBPM = 666;
            songLengthMin = 6;
            songLengthSec = 66;

            //List of instrument IDs that are present in the song
            List<string> instruments = new List<string>()
            {
                "drums",
                "guitar",
                "bass",
                "strings"
            };


            //List of emotions and tones associated with the song
            //Range for each emotion should be 0 (not sad at all) to 1(definitely sad)
            List<double> emotions = new List<double>()
            {
                //anger
                0.9,
                //fear
                0.7,
                //joy
                0.05,
                //sadness
                0.8,
                //analytical
                0.05,
                //confident
                0.3,
                //tentative
                0.2
            };


            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + songID + ".data";
            FileStream stream = new FileStream(path, FileMode.Create);

            //List of the mp3 file names for each instrument
            List<string> clipNames = new List<string>()
            {
                "Cello_AJourneyToHeaven",
                "Piano_AJourneyToHeaven",
                "Synth_AJourneyToHeaven",
                "Strings_AJourneyToHeaven"
            };

            //List of the the cues for each instrument
            List<float> allCues = aJourneyToHeavenCues();

            //Creating binary file
            SongData aJourneyToHeaven = new SongData(songName, songAuthor, instruments, allCues, clipNames, songBPM, songLengthMin, songLengthSec);
            formatter.Serialize(stream, aJourneyToHeaven);
            stream.Close();

            songDataFilenameList.Add(songID + ".data");


            //save the emotions to a seperate file
            BinaryFormatter formatter2 = new BinaryFormatter();
            path = Application.persistentDataPath + "/" + songID + ".emo";
            FileStream stream2 = new FileStream(path, FileMode.Create);
            formatter2.Serialize(stream2, emotions);
            stream.Close();
        }


        //Save reference txt
        //if (!System.IO.File.Exists(Application.persistentDataPath + "/saveFiles.txt"))
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/saveFiles.txt", false);
            foreach(string name in songDataFilenameList){
                Debug.Log("Preloader: Start --- saving " + name + "to the list of songs");
                writer.WriteLine(name);
            }
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





    /// <summary>
    /// Hardcoding of instrument cue timing data
    /// </summary>
    /// <returns>Float list of instrument cues seperated by -1 </returns>
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

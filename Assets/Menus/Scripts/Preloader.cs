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
        if (!System.IO.File.Exists(Application.persistentDataPath + "/FurElise.data"))
        {
            Debug.Log("Found txt file");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/FurElise.data";
            FileStream stream = new FileStream(path, FileMode.Create);
            List<string> instruments = new List<string>();
            instruments.Add("xylophone");
            instruments.Add("piano");
            instruments.Add("violins");
            SongData furElise = new SongData("Fur Elise", instruments);
            // Creating binary file
            formatter.Serialize(stream, furElise);
            stream.Close();  
        }

        if (!System.IO.File.Exists(Application.persistentDataPath + "/saveFiles.txt")) 
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/saveFiles.txt", false);
            writer.WriteLine("FurElise.data");
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
}

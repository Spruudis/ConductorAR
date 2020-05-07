using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

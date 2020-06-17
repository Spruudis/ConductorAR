using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private Text hi;
    [SerializeField]
    private Text greeting;
    [SerializeField]
    private Image mic;
    [SerializeField]
    private Image micRed;

    [SerializeField]
    private float revealTime;
    [SerializeField]
    private float pauseTime;


    private bool reveal = false;
    private bool finished = false;

    private float startTime;


    public bool Reveal
    {
        get{ return reveal; }
        set
        {
            reveal = value;
            startTime = Time.time;
        }
    }

    public bool Finished
    {
        get { return finished; }
    }

    void Update()
    {
        if (reveal && !finished)
        {
            Color tempColor = hi.color;
            tempColor.a = Time.time - (startTime + revealTime);
            hi.color = tempColor;

            tempColor.a = Time.time - (startTime + revealTime + pauseTime + revealTime);
            greeting.color = tempColor;

            tempColor.a = Time.time - (startTime + revealTime + pauseTime + revealTime + pauseTime + revealTime);
            mic.color = tempColor;

            tempColor.a = Time.time - (startTime + revealTime + pauseTime + revealTime + pauseTime + revealTime + revealTime);
            micRed.color = tempColor;

            if(Time.time >= startTime + revealTime + pauseTime + revealTime + pauseTime + revealTime + revealTime + 1)
            {
                finished = true;
            }
        }
    }
}

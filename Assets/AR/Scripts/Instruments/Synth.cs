using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Synth : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    AudioSource cueOutMistake;
    public AudioClip mistakeSound;
    MeshRenderer myRenderer;
    List<float> cues;
    int cueIndex;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        cueIndex = 0;
        cueOutMistake = gameObject.AddComponent<AudioSource>();
        cueOutMistake.clip = mistakeSound;
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = Color.black; 
    }

    public void Initialise(List<float> inputCues, string clipName)
    {
        cues = inputCues;
        AudioClip songClip = Resources.Load<AudioClip>("Sounds/" + clipName);
        m_MyAudioSource.clip = songClip;
        m_MyAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MyAudioSource.isPlaying)
        {
            if((m_MyAudioSource.time - cues[cueIndex]) > 1.5){
                if(cueIndex % 2 == 0){
                    userMistakeCueIn();
                } else{
                    userMistakeCueOut();
                }
                cueIndex++;
            } else if((cues[cueIndex] - m_MyAudioSource.time) < 0.75 && (cues[cueIndex] - m_MyAudioSource.time) > 0.5){
                Debug.Log("3");
                myRenderer.material.color = Color.red;
            } else if((cues[cueIndex] - m_MyAudioSource.time) < 0.5 && (cues[cueIndex] - m_MyAudioSource.time) > 0.25){
                Debug.Log("2");
                myRenderer.material.color = Color.yellow;
            } else if((cues[cueIndex] - m_MyAudioSource.time) < 0.25){
                Debug.Log("1");
                myRenderer.material.color = Color.green;
            }
        }
        
    }

    public void OnTap()
    {
        Debug.Log("Synth: OnSwipeDown: recieved tap gesture command");
        if (Math.Abs(m_MyAudioSource.time - cues[cueIndex]) < 1.5)
        {
            if (cueIndex % 2 == 0)
            {
                userCueIn();
            }
            else
            {
                userCueOut();
            }
        }

    }

    public void OnSwipeUp()
    {
        Debug.Log("Synth: OnSwipeUp: recieved swipe up gesture command");
    }

    public void OnSwipeDown()
    {
        Debug.Log("Synth: OnSwipeDown: recieved swipe down gesture command");
    }


    private void userMistakeCueIn(){
        m_MyAudioSource.mute = false;
        myRenderer.material.color = Color.black;
        cueOutMistake.Play();
    }

    private void userMistakeCueOut(){
        m_MyAudioSource.mute = true;
        myRenderer.material.color = Color.black;
        cueOutMistake.Play();
    }

    private void userCueIn(){
        m_MyAudioSource.mute = false;
        cueIndex++;
        myRenderer.material.color = Color.black;
    }

    private void userCueOut(){
        m_MyAudioSource.mute = true;
        cueIndex++;
        myRenderer.material.color = Color.black;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InstrumentControl : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    // List of cues
    List<float> cues;
    // Variable that holds the index of the next cue
    int cueIndex;
    // BPM of song
    float BPM;
    // amount of seconds for four beats
    float fourBeatsTime;

    bool cueAnimationOn;
    string clip;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        cueIndex = 0;
    }

    public void Initialise(List<float> inputCues, string clipName, int inputBPM)
    {
        // Load cues and clip, and start the song
        clip = clipName;
        cues = inputCues;
        BPM = inputBPM;
        fourBeatsTime = 60/(BPM/4);
        cueAnimationOn = false;
        AudioClip songClip = Resources.Load<AudioClip>("Sounds/" + clipName);
        m_MyAudioSource.clip = songClip;
        m_MyAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MyAudioSource.isPlaying)
        {
            if((cues[cueIndex] - m_MyAudioSource.time) <= fourBeatsTime){
                if(!cueAnimationOn){
                cueAnimationOn = true;
                    if(cueIndex % 2 == 0){
                        // call animation for cue in. There should be no instance where it's time for a cue in 
                        // and the musicians are already playing
                        // TO-DO: Call Cue in animation function
                        //Debug.Log("Cue In");
                    } else {
                        //Debug.Log("Cue Out");
                        // This is the case for a cue out
                        if(m_MyAudioSource.mute){
                            // In this case, the avatars are already not playing. Shall we have an animation
                            // different to the cue out animation just to show that this was when they would
                            // have cued out?
                            //Debug.Log("Cue out opportunity missed");
                        } else{
                            // call animation for cue out. 
                            // TO-DO: Call Cue out animation function
                            //Debug.Log("Cue out animation");
                        }
                    }
                }
            }
            if((m_MyAudioSource.time - cues[cueIndex]) > fourBeatsTime/2){
                if(cueIndex % 2 != 0 && !m_MyAudioSource.mute){
                    // In this case, the user is late to cue out, so the musicians will stop
                    // and let's have them have a little grumble
                    m_MyAudioSource.mute = true;
                    //TO-DO: Call grumbling animation function
                }
                cueAnimationOn = false;
                cueIndex++;
            }
        }
    }

    public void OnTap()
    {
        if(m_MyAudioSource.isPlaying)
        {
            if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/4){
                Debug.Log("Cue on time");
                m_MyAudioSource.mute = !m_MyAudioSource.mute;
                // We could have a lovely happy animation or something cute
                // TO-DO: Call happy animation function
            }  else if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/2){
                m_MyAudioSource.mute = !m_MyAudioSource.mute;
                m_MyAudioSource.mute = false;
                // Call disgruntled animation as slightly early/late
                // TO-DO: CALL DISGRUNTLED ANIMATION

            } else{
                if(m_MyAudioSource.mute){
                    if(cueIndex % 2 == 0){
                        // nothing to cue in, avatars have a bit of a grumble
                        // TO-DO: Add avatars grumble animation function call
                    } else{
                        m_MyAudioSource.mute = false;
                        // Turn off constant grumbling, since the avatars should be grumbling while not playing
                        // TO-DO: Turn off Grumbling animation function
                    }
                } else{
                    if(cueIndex % 2 != 0){
                        m_MyAudioSource.mute = true;
                        // Turn on constant grumbling, they're a bit annoyed you've cut them off
                        // TO-DO: Turn on constant grumbling function, if we can make this constant
                    }
                }
            }
        }
        
    }

    public void OnSwipeUp()
    {
        Debug.Log("Cello: OnSwipeUp: recieved swipe up gesture command");
    }

    public void OnSwipeDown()
    {
        Debug.Log("Cello: OnSwipeDown: recieved swipe down gesture command");
    }

    public void PauseMusic()
    {
        m_MyAudioSource.Pause();
    }

    public void UnpauseMusic()
    {
        m_MyAudioSource.UnPause();
    }

    public void RestartMusic()
    {
        m_MyAudioSource.Play();
    }

}

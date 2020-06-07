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

    bool songInit;
    bool songPaused;
    bool cued;

    GameObject canvasRoot;
    GameObject endScreen;
    GameObject backButton;
    GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        cueIndex = 0;
        songInit = false;
        cueAnimationOn = false;
        songPaused = false;
        canvasRoot = GameObject.Find("Canvas");
        cued = false;
    }

    public void Initialise(List<float> inputCues, string clipName, int inputBPM)
    {
        // Load cues and clip, and start the song
        clip = clipName;
        cues = inputCues;
        BPM = inputBPM;
        fourBeatsTime = 60/(BPM/4);
        AudioClip songClip = Resources.Load<AudioClip>("Sounds/" + clipName);
        m_MyAudioSource.clip = songClip;
        m_MyAudioSource.Play();
        songInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(songInit)
        {
            if(cueIndex < cues.Count){
                if((cues[cueIndex] - m_MyAudioSource.time) <= fourBeatsTime){
                    if(!cueAnimationOn){
                        cueAnimationOn = true;
                        if(cueIndex % 2 == 0){
                            // call animation for cue in. There should be no instance where it's time for a cue in 
                            // and the musicians are already playing
                            // TO-DO: Call Cue in animation function
                            Debug.Log("Cue In animaton called: " + clip);
                        } else {
                            Debug.Log("Cue Out: " + clip);
                            // This is the case for a cue out
                            if(m_MyAudioSource.mute){
                                // In this case, the avatars are already not playing. Shall we have an animation
                                // different to the cue out animation just to show that this was when they would
                                // have cued out?
                                //Debug.Log("Cue out opportunity missed");
                                Debug.Log("Musicians already not playing: " + clip);
                            } else{
                                // call animation for cue out. 
                                // TO-DO: Call Cue out animation function
                                //Debug.Log("Cue out animation");
                                Debug.Log("Cue out animation called: " + clip);
                            }
                        }
                    }
                }
                if((m_MyAudioSource.time - cues[cueIndex]) > fourBeatsTime/2){
                    if(!cued){
                        if(cueIndex % 2 != 0 && !m_MyAudioSource.mute){
                            // In this case, the user is late to cue out, so the musicians will stop
                            // and let's have them have a little grumble
                            m_MyAudioSource.mute = true;
                            //TO-DO: Call grumbling animation function
                            Debug.Log("User does not cue out so musicians must stop themselves");
                        } else if(cueIndex % 2 == 0 && m_MyAudioSource.mute){
                            Debug.Log("User does not cue in so musicians have a bit of a grumble");
                            //TO-DO: Call grumbling animation function
                        }
                        cueIndex++;
                    }
                    cued = false;
                    cueAnimationOn = false;
                }
            }

            if(!m_MyAudioSource.isPlaying && !songPaused){
                songInit = false;
                endScreen = canvasRoot.transform.Find("EndMenuBackground").gameObject;
                backButton = canvasRoot.transform.Find("BackButton").gameObject;
                pauseButton = canvasRoot.transform.Find("PauseButton").gameObject;
                endScreen.SetActive(true);
                backButton.SetActive(false);
                pauseButton.SetActive(false);
            }
        }
    }

    public void OnTap()
    {
        if(m_MyAudioSource.isPlaying && cueIndex < cues.Count)
        {
            Debug.Log("Tapped on: " + clip);
            if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/4 && !cued){
                Debug.Log("Cue on time: " + clip);
                m_MyAudioSource.mute = !m_MyAudioSource.mute;
                cueIndex++;
                cued = true;
                // We could have a lovely happy animation or something cute
                // TO-DO: Call happy animation function
            }  else if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/2 && !cued){
                m_MyAudioSource.mute = !m_MyAudioSource.mute;
                m_MyAudioSource.mute = false;
                Debug.Log("Slightly early/late cue");
                cueIndex++;
                cued = true;
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
                        Debug.Log("User Cued in " + clip);
                    }
                } else{
                    if(cueIndex % 2 != 0){
                        m_MyAudioSource.mute = true;
                        // Turn on constant grumbling, they're a bit annoyed you've cut them off
                        // TO-DO: Turn on constant grumbling function, if we can make this constant
                        Debug.Log("User Cued out " + clip);
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
        songPaused = true;
    }

    public void UnpauseMusic()
    {
        m_MyAudioSource.UnPause();
        songPaused = false;
    }

    public void RestartMusic()
    {
        m_MyAudioSource.Play();
        songInit = true;
    }

}

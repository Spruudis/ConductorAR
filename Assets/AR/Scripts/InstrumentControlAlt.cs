using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InstrumentControlAlt : MonoBehaviour
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

    GameObject canvasRoot;
    GameObject endScreen;
    GameObject backButton;
    GameObject pauseButton;

    AnimationScriptAlt animationControl;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        animationControl = GetComponent<AnimationScriptAlt>();
        cueIndex = 0;
        songInit = false;
        cueAnimationOn = false;
        songPaused = false;
        canvasRoot = GameObject.Find("Canvas");
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
        // If the song has been initialised, eg the game has started
        {
            if(cueIndex < cues.Count)
            //Only update if the cue index is less than the number of cues, prevents exception
            {
                if((cues[cueIndex] - m_MyAudioSource.time) <= fourBeatsTime){
                // If the song is four beats before the cue, start the animation.
                    callCueAnimations();
                }
                if((m_MyAudioSource.time - cues[cueIndex]) > fourBeatsTime/2){
                // If the song has progressed more than half a bar, call update Cue Status function
                // The update cue status function checks if the user successfully cued.
                // If not, then the cue index must be advanced and animations must occur to show
                // Musicians are not happy 
                    updateCueStatus();
                }
            }

            if(!m_MyAudioSource.isPlaying && !songPaused){
                // If the song has stopped playing and the user didn't pause it, the game is
                // over. Call the end game function to end the game.
                endGame();
            }
        }
    }

    public void OnTap()
    {
        if(m_MyAudioSource.isPlaying && cueIndex < cues.Count)
        {
            Debug.Log("Tapped on: " + clip);
            if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/4){
                // If the user has cued in within a beat of the correct cue timing, this is perfect
                correctCueTiming();
            }  else if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < fourBeatsTime/2){
                // If the user has cued in within two beats of the correct cue timing, this is slightly
                // off and hence it is slightly off cue timing. The musicians have a grumble but do the correct
                // cue action
                slightlyOffCueTiming();
            } else{
                noCueActions();
            }
        }
        
    }

    private void callCueAnimations()
    {
        if(!cueAnimationOn){
        // Since update will call multiple times will the audio is within four beats of the cue,
        // we have a simple if statement that checks if the cue animation has already been called.
        // If it hasn't then the function will run, whereas if it has then there is no need to do anything.
            cueAnimationOn = true;
            if(cueIndex % 2 == 0){
            // call animation for cue in. There should be no instance where it's time for a cue in 
            // and the musicians are already playing
                Debug.Log("Cue In animation called: " + clip);
                animationControl.triggerJump(1/fourBeatsTime);
            } else {
                Debug.Log("Cue Out: " + clip);
                // This is the case for a cue out
                if(m_MyAudioSource.mute){
                // In this case, the avatars are already not playing. Shall we have an animation
                // different to the cue out animation just to show that this was when they would
                // have cued out?
                    Debug.Log("Musicians already not playing: " + clip);
                } else{
                // call animation for cue out. 
                // TO-DO: Call Cue out animation function
                // Debug.Log("Cue out animation");
                    Debug.Log("Cue out animation called: " + clip);
                    animationControl.triggerJump(1/fourBeatsTime);
                }
            }
        }
    }

    private void updateCueStatus()
    {
        // Only update cue status if the user didn't cue in, this function is only used in
        // cases where there was a cue but the user did not initiate it, therefore the cue
        // index and animations must be called to demonstrate a missed cue.
        if(cueIndex % 2 != 0 && !m_MyAudioSource.mute){
            // In this case, the user did not cue out, so the musicians will stop
            // and let's have them have a little grumble
            m_MyAudioSource.mute = true;
            Debug.Log("User does not cue out so musicians must stop themselves");
            animationControl.triggerAnnoyed();
            animationControl.triggerIdle();
        } else if(cueIndex % 2 == 0 && m_MyAudioSource.mute){
            // In this case, the user did not cue in so the musicians have a grumble but stay silent.
            Debug.Log("User does not cue in so musicians have a bit of a grumble");
            animationControl.triggerAnnoyed();
            animationControl.triggerWave();
        }
        cueIndex++;
        cueAnimationOn = false;
    }

    private void endGame()
    {
        // This function activates the pop up end game menu where the user can press
        // a restart button to restart the game or press the quit button. Also,
        // the back button and pause button are set inactive as these should disappear
        // at end game.
        songInit = false;
        endScreen = canvasRoot.transform.Find("EndMenuBackground").gameObject;
        backButton = canvasRoot.transform.Find("BackButton").gameObject;
        pauseButton = canvasRoot.transform.Find("PauseButton").gameObject;
        endScreen.SetActive(true);
        backButton.SetActive(false);
        pauseButton.SetActive(false);
    }

    private void correctCueTiming()
    {
        //The user has cued in at the correct timing. This function checks if it was a cue in or cue out
        // and starts or stops the musicians playing depending on that fact. Furthermore, it changes the
        // boolean value cued to true, letting the update know that the user correctly cued in.
        Debug.Log("Cue on time: " + clip);
        if(cueIndex % 2 == 0){
            // If it was a cue in, start musicians playing animation
            animationControl.triggerPlay();
        } else{
            // If it was a cue out, start musicians idle animation
            animationControl.triggerIdle();
        }
        m_MyAudioSource.mute = !m_MyAudioSource.mute;
        // update cue index to look at next cue
        cueIndex++;
        // user has correctly cued in
        cueAnimationOn = false;
        // We could have a lovely happy animation or something cute
        // TO-DO: Call happy animation function
    }

    private void slightlyOffCueTiming()
    {
        // The user has cued slightly early/late, the musicians have an annoyed animation
        // and then begin either playing or stop playing depending on whether it is a cue in
        // or cue out. The cue index is then updated and the boolean cued is set to true to let the update
        // function know that the user did cue.
        Debug.Log("Slightly early/late cue");

        // Trigger annoyed animation
        animationControl.triggerAnnoyed();
        // Start/stop playing
        m_MyAudioSource.mute = !m_MyAudioSource.mute;
        if(cueIndex % 2 == 0){
            // If it was a cue in, start playing animation
            animationControl.triggerPlay();
        } else{
            // If it was a cue out, start idle animation
            animationControl.triggerIdle();
        }
        cueIndex++;
        cueAnimationOn = false;
    }

    private void noCueActions()
    {
        // This function is called when a cue in animation has not been played but the user experiments
        // with the cueing in and out of the instruments. For example, the musicians are playing but
        // there is no cue out but the user taps on them anyway. This cues them out but they have a grumble
        // as they can still play. They then start waving to show that they can be cued in and play more.
        if(m_MyAudioSource.mute){
            // If the musicians are not playing
            if(cueIndex % 2 == 0){
                // If the next cue is a cue in, then at present there is nothing to cue in, 
                // Avatars have a bit of a grumble
                Debug.Log("Nothing to cue in: Trigger Annoyed animation");
                animationControl.triggerAnnoyed();
            } else{
                // If the next cue is a cue out, then the musicians can curently be playing something but aren't.
                // The musicians start playing again and they stop waving.
                m_MyAudioSource.mute = false;
                Debug.Log("Musicians start playing again: Turn off waving and turn on playing");
                // Turn off constant waving, since the avatars should be waving when they can be playing
                animationControl.triggerPlay();
            }
        } else{
            // If the musicians are playing
            if(cueIndex % 2 != 0){
                // If the next cue is a cue out, the musicians are currently playing and now will be cut off
                // The musicians stop playing.
                m_MyAudioSource.mute = true;
                // Turn on constant waving and annoyed animation, they're a bit annoyed you've cut them off
                Debug.Log("You've cut off the musicians: Trigger annoyed and wave");
                animationControl.triggerAnnoyed();
                animationControl.triggerWave();
            }
        }
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

    public void OnSwipeUp()
    {
        Debug.Log("Cello: OnSwipeUp: recieved swipe up gesture command");
    }

    public void OnSwipeDown()
    {
        Debug.Log("Cello: OnSwipeDown: recieved swipe down gesture command");
    }

}

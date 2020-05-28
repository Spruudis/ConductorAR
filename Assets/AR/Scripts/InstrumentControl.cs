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
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        cueIndex = 0;
    }

    public void Initialise(List<float> inputCues, string clipName)
    {
        // Load cues and clip, and start the song
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
            if((cues[cueIndex] - m_MyAudioSource.time) < 1){
                if(cueIndex % 2 == 0){
                    // call animation for cue in. There should be no instance where it's time for a cue in 
                    // and the musicians are already playing
                    // TO-DO: Call Cue in animation function
                } else {
                    // This is the case for a cue out
                    if(m_MyAudioSource.mute){
                        // In this case, the avatars are already not playing. Shall we have an animation
                        // different to the cue out animation just to show that this was when they would
                        // have cued out?
                    } else{
                        // call animation for cue out. 
                        // TO-DO: Call Cue out animation function
                    }
                }
            }
            if((m_MyAudioSource.time - cues[cueIndex]) > 1){
                if(cueIndex % 2 != 0 && !m_MyAudioSource.mute){
                    // In this case, the user is late to cue out, so the musicians will stop
                    // and let's have them have a little grumble
                    m_MyAudioSource.mute = !m_MyAudioSource.mute;
                }
                cueIndex++;
            }
        }
    }

    public void OnMouseDown()
    {
        if(m_MyAudioSource.isPlaying)
        {
            if(Math.Abs(cues[cueIndex] - m_MyAudioSource.time) < 1){
                m_MyAudioSource.mute = !m_MyAudioSource.mute;
                // We could have a lovely happy animation or something cute
                // TO-DO: Call happy animation function
            } else{
                if(m_MyAudioSource.mute){
                    if(cueIndex % 2 == 0){
                        // nothing to cue in, avatars have a bit of a grumble
                        // TO-DO: Add avatars grumble animation function call
                    } else{
                        m_MyAudioSource.mute = !m_MyAudioSource.mute;
                        // Turn off constant grumbling, since the avatars should be grumbling while not playing
                        // TO-DO: Turn off Grumbling animation function
                    }
                } else{
                    if(cueIndex % 2 != 0){
                        m_MyAudioSource.mute = !m_MyAudioSource.mute;
                        // Turn on constant grumbling, they're a bit annoyed you've cut them off
                        // TO-DO: Turn on constant grumbling function, if we can make this constant
                    }
                }
            }
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
	// Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    void triggerJump(){animator.SetTrigger("Jump");}
    void triggerWave(){animator.SetTrigger("Wave");}
    void triggerHappy(){animator.SetTrigger("Happy");}
    void triggerSad(){animator.SetTrigger("Sad");}
    void triggerCountdown(){animator.SetTrigger("Countdown");}
    void toggleStand(){
        if(animator.GetBool("Stand")==true){animator.SetBool("Stand",false);}
        else{animator.SetBool("Stand",true);}
    }
    void togglePlay(){
        if(animator.GetBool("Play")==true){animator.SetBool("Play",false);}
        else{animator.SetBool("Play",true);}
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Clock",animator.GetFloat("Clock")+Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.J)){triggerJump();}
        if(Input.GetKeyDown(KeyCode.W)){triggerWave();}
        if(Input.GetKeyDown(KeyCode.H)){triggerHappy();}
        if(Input.GetKeyDown(KeyCode.S)){triggerSad();}
        if(Input.GetKeyDown(KeyCode.C)){triggerCountdown();}
        if(Input.GetKeyDown(KeyCode.UpArrow)){toggleStand();}
        if(Input.GetKeyDown(KeyCode.P)){togglePlay();}
    }
}

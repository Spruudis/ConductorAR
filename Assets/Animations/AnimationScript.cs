using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    int clock=0;
    int framerate=64;
    int counter=0;
    int wavewait=10;
	// Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    public void triggerJump(float speed){
        animator.speed = speed;
        animator.SetTrigger("Jump");
    }
    public void triggerWave(){
        animator.speed = 1;
        animator.SetTrigger("Wave");
    }
    public void triggerPlay(){
        animator.speed = 1;
        animator.SetTrigger("Play");
    }
    public void triggerAnnoyed(){
        animator.speed = 1;
        animator.SetTrigger("Annoyed");
    }
    public void triggerIdle(){
        animator.speed = 1;
        animator.SetTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        animator.SetInteger("Clock",clock);
        animator.SetInteger("Counter",counter);
        clock++;
        clock%=framerate;
        if(clock==0){counter++;}
        if(Input.GetKeyDown(KeyCode.Space)){
            triggerJump();
        }
        if(Input.GetKeyDown(KeyCode.X)){
            triggerWave();
        }
        */
    }
}

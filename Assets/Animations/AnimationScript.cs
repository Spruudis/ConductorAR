using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    int framerate=64;
    float idletime=0;
	// Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    void triggerJump(){
        animator.SetTrigger("Jump");
        idletime=0;
    }
    void triggerWave(){
        animator.SetTrigger("Wave");
        idletime=0;
    }

    // Update is called once per frame
    void Update()
    {
        idletime+=Time.deltaTime;
        animator.SetFloat("Clock",idletime);
        if(Input.GetKeyDown(KeyCode.Space)){
            triggerJump();
        }
        if(Input.GetKeyDown(KeyCode.X)){
            triggerWave();
        }
    }
}

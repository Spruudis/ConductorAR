using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockReset : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("Clock",0);
    }
}

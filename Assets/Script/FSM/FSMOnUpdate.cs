using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnUpdate : StateMachineBehaviour
{
    public string[] messages;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (string message in messages)
        {
            animator.SendMessageUpwards(message);
        }
    }

}

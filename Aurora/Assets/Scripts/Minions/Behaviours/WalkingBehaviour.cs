using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MinionController;
public class WalkingBehaviour : MinionBehaviour
{
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator,stateInfo,layerIndex);
        if(target != null){
            minion.goToPosition(target.transform.position - new Vector3(1,0,1));
            if(Vector3.Distance(minion.transform.position,target.transform.position) <= range){
                animator.SetBool("attackRange",true);
            }
        }else{
            animator.SetBool("playerNear",true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

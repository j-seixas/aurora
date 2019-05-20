using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCooldownBehaviour : MinionBehaviour
{
  

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator,stateInfo,layerIndex);
        if(target != null && Vector3.Distance(minion.transform.position,target.transform.position) >= minion.range){
           animator.SetBool("attackRange",false);
        }
        else
            animator.SetBool("didAttack",false);    
    }

    private void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.SetBool("attacked",false);
    }

   

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

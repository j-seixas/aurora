using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimationBehaviour : StateMachineBehaviour
{
    private Attack player;

    public string type;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       this.player = animator.GetComponentInParent<Attack>();
       animator.SetBool("Attack",false);
       animator.GetComponentInParent<PlayerController>().PlaySoundAttack(type);  //plays sound effect for each type of attack

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetButtonDown("Attack")) {
            Debug.Log("HELLOexit");
            animator.SetBool("Attack",true);
        }else{
            animator.SetBool("Attack",false);
        } 
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

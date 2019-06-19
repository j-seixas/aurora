using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimationBehaviour : StateMachineBehaviour
{
    private PlayerController player;

    public string type;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       this.player = animator.GetComponentInParent<PlayerController>();
       animator.SetBool("Attack",false);
       this.player.PlaySoundAttack(type);  //plays sound effect for each type of attack

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Input.GetButtonDown("Attack") && type != "3") {
            animator.SetBool("Attack",true);
        }
    }

}

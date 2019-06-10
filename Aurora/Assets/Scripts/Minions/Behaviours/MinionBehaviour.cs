using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MinionController;
public class MinionBehaviour : StateMachineBehaviour
{
    

    protected Collider target;
    protected MinionController minion;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.minion = (MinionController) animator.GetComponentInParent<MinionController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(minion == null){
            minion = (MinionController) animator.GetComponentInParent<MinionController>();
            animator.SetBool("spawnCooldown", minion.isInSpawnCooldown);
            return;
        }
        animator.SetBool("spawnCooldown", minion.isInSpawnCooldown);
        target = minion.checkForPlayer();
    }

}

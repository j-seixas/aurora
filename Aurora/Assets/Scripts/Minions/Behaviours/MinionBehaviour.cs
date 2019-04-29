using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MinionController;
public class MinionBehaviour : StateMachineBehaviour
{
    protected float range = 3f;

    protected Collider target;
    protected MinionController minion;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        minion = (MinionController) animator.GetComponentInParent<MinionController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       target = minion.checkForPlayer();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MinionController;
public class WalkingBehaviour : MinionBehaviour {

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate (animator, stateInfo, layerIndex);
        if (target != null) {
            if (Vector3.Distance (minion.transform.position, target.transform.position) <= minion.range) {
                minion.goToPosition (minion.transform.position);
                animator.SetBool ("attackRange", true);
            } else {
                minion.goToPosition (target.transform.position);
            }

        } else {
            animator.SetBool ("playerNear", false);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCooldownBehaviour : MinionBehaviour
{

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        animator.SetBool("spawnCooldown", minion.isInSpawnCooldown);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinionController;

public class IdleBehaviour : MinionBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator,stateInfo,layerIndex);
        if(target != null){
            animator.SetBool("playerNear",true);
        }
    }

}

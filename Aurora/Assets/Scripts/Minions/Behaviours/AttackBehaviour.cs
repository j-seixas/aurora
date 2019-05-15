using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MinionBehaviour
{
   


     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateEnter(animator,stateInfo,layerIndex);
       animator.SetBool("attacked",false);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator,stateInfo,layerIndex);
        if(target != null && Vector3.Distance(minion.transform.position,target.transform.position) >= minion.range){
           animator.SetBool("attackRange",false);
        }else if(!animator.GetBool("attacked")){
           if(!minion.Attack()){
               animator.SetBool("didAttack",true);
               animator.SetBool("attacked",true);
           }  
       }
    }
    

 

  
  


    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

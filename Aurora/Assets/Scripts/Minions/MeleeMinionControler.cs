using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static MinionController;

public class MeleeMinionControler : MinionController {
    
    public override bool Attack(){
        //Debug.Log("Attack melee");
        //StartCoroutine(timeout());
        //Debug.Log(this.damage);
        //THIS IS ONLY UNTIL MINION HAS A WEAPON OR SOMETHING
        //NOT CHECKING IF ATTACK HITS
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ReceiveDamage(this.damage);
        return false;
        
    }
}
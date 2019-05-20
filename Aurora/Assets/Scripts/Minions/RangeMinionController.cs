using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using static MinionController;

public class RangeMinionController: MinionController {
    
    
    public override bool Attack(){
        GameObject fireball = ObjectPooler.SharedInstance.GetPooledObject("Fireball");
        fireball.transform.position = new Vector3(this.transform.position.x,transform.position.y,transform.position.z);
        RangeAttackController attack = fireball.GetComponent<RangeAttackController>();
        Collider player = this.checkForPlayer();
        if(player == null)
            return false;
        attack.setDirection(getDirection(this.transform.position,player.gameObject.transform.position));
        return false;
    }

    public Vector3 getDirection(Vector3 from, Vector3 to){
        return (to-from).normalized;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static MinionController;

public class MeleeMinionControler : MinionController {
    
    public override void Attack(){
        Debug.Log("Attack melee");
    }
}
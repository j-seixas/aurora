using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using static MinionController;

public class RangeMininionController: MinionController {
    
    public override void Attack(){
        Debug.Log("Attack ranged");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static MinionController;

public class MeleeMinionControler : MinionController {
    
    public override bool Attack(){       
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().UpdateAttribute(GameManager.Attributes.Health, -this.damage);
        AudioManager.Instance.PlaySFX("minion_attack_head"); //aurora sound
        return false;
    }
}
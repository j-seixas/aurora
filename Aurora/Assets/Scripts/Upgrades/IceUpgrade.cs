using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;
    public GameObject player, freezeAOE;

    public override void Active() {
        Instantiate(freezeAOE, player.transform.position, Quaternion.identity);
    }

    public override void Passive() { 
        Debug.Log("FREEZE PASSIVE");
    }

    public override void LevelUp() {
        this.level++;
    } 
}

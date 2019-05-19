using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;

    public override void Active() {
        Debug.Log("FREEZE ACTIVE");
    }

    public override void Passive() { 
        Debug.Log("FREEZE PASSIVE");
    }

    public override void LevelUp() {
        this.level++;
    } 
}

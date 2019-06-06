using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;

    private void Start() {
        this.type = Type.Fire;
    }

    public override void Active() {
        Debug.Log("FIRE ACTIVE");
    }

    public override void Passive() { 
        Debug.Log("FIRE PASSIVE");
    }

    public override void LevelUp() {
        this.level++;
    } 

}
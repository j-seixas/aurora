using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;

    public override void Active() {
        Debug.Log("LIFE ACTIVE");
    }

    public override void Passive() { 
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;
    public GameObject freezeAOE;
    private GameObject player;

    private void Start() {
        this.type = Type.Ice;
    }

    public override void Active() {
        player = GameObject.FindWithTag("Player");
        Instantiate(freezeAOE, player.transform.position, Quaternion.identity);
    }

    public override void Passive() { 
        Debug.Log("FREEZE PASSIVE");
    }

    public override void LevelUp() {
        this.level++;
    } 
}

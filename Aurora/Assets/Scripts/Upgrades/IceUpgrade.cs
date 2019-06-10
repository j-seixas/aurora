using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;
    private float[] slowByLevel = {0.3f, 0.4f, 0.45f, 0.5f, 0.5f};
    private float[] durationByLevel = {2.0f, 2.2f, 2.4f, 2.5f, 3.0f};

    public GameObject freezeAOE;
    private GameObject player;

    private void Start() {
        this.type = Type.Ice;
        this.cooldown = new float[5] {5.0f, 4.5f, 4.0f, 3.5f, 3.0f};
    }

    public (float slow, float duration) GetPassiveParameters() {
        return (this.slowByLevel[this.level], this.durationByLevel[this.level]);
    }

    public override void Active() {
        if (!this.isActiveInCooldown) {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(this.ElapseActiveCooldown(this.cooldown[this.level]));
        }
    }

    public override void Passive() { 
        Debug.Log("FREEZE PASSIVE");
    }

    public override void LevelUp() {
        // Attempt to upgrade level and make every upgrade status change.
        if (this.UpgradeLevel()) {
            // Add gem specific logic here.
        }
    } 
}

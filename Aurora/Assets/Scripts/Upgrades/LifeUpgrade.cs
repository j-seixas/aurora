using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade {
    [Header("Specific")]
    public int healthGain;
    private int[] healthGainByLevel = {5, 10, 15, 20, 25};
    public GameObject shieldChargeObject;

    private void Start() {
        this.type = Type.Life;
    }

    private void Update() {
        if (!IsInvoking("Passive") && this.level > 0)
            InvokeRepeating("Passive", this.tick, this.tick);
    }

    public override void Active() {
        Instantiate(shieldChargeObject, transform.position, Quaternion.identity);
    }

    public override void Passive() {
        GetComponentInParent<PlayerController>().UpdateAttribute(GameManager.Attributes.Health, this.healthGain);

        foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>()) {
            particle.Play();
        }
    }

    public override void LevelUp() {
        // Attempt to upgrade level and make every upgrade status change.
        if (this.UpgradeLevel()) {
            this.healthGain = this.healthGainByLevel[this.level];  // Gem specific logic.
        }
    } 
}
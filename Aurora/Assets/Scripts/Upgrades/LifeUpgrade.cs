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
        int balance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetAttribute(GameManager.Attributes.Spirits);

        if (balance >= this.spiritCostByLevel[this.level]) {
            this.healthGain = this.healthGainByLevel[this.level];
            this.level++;
            this.UpdateHUDElements();
        } else { Debug.Log("Not enough balance!"); }
    } 

}
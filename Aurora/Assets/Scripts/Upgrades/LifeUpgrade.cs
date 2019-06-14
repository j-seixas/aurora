using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade {
    [Header("Specific")]
    public int healthGain;
    private int[] healthGainByLevel = {5, 10, 15, 20, 25};

    public GameObject shieldChargeObject;
    private List<GameObject> shields = new List<GameObject>();

    private void Start() {
        this.type = Type.Life;
    }

    private void Update() {
        if (!IsInvoking("Passive") && this.level > 0)
            InvokeRepeating("Passive", this.tick, this.tick);
    }


    public override void Active() {
        if (this.isActiveInCooldown) {
            return;
        }

        for (int i = 0; i < 3; i++) {
            GameObject obj = Instantiate(shieldChargeObject, transform.position + Vector3.forward * 2, Quaternion.identity);

            GameObject player = GameObject.FindGameObjectWithTag("PlayerBody");
            obj.transform.RotateAround(player.transform.position, Vector3.up, i * 120);

            obj.transform.SetParent(transform.root);
            this.shields.Add(obj);
        }
        this.isActiveInCooldown = true;
    }

    public override void Passive() {
        GetComponentInParent<PlayerController>().UpdateAttribute(GameManager.Attributes.Health, this.healthGain);

        foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>()) {
            particle.Play();
        }
    }

    public void RestoreActiveCharges() =>
        this.isActiveInCooldown = false;

    public void BreakShield() {
        if (this.shields.Count > 0) {
            AudioManager.Instance.PlaySFX("shield"); // shield breaking sound effect
            Destroy(this.shields[0]);
            this.shields.RemoveAt(0);
        } 
    }

    public bool HasShieldActive() =>
        this.shields.Count > 0;

    public override void LevelUp() {
        // Attempt to upgrade level and make every upgrade status change.
        if (this.UpgradeLevel()) {
            this.healthGain = this.healthGainByLevel[this.level];  // Gem specific logic.
        }
    }

    public override string GetBillboardText() {
        string passive1;

        if (this.level == 0) {
            passive1 = "Health gain: 0 -> " + this.healthGainByLevel[this.level].ToString() + "\n";
        } else {
            passive1 = "Health gain: " + this.healthGainByLevel[this.level-1].ToString() + " -> " + this.healthGainByLevel[this.level].ToString() + "\n";
        }

        string cost = "Cost: " + this.spiritCostByLevel[this.level].ToString () + " spirits";
        return passive1 + cost;
    }
}
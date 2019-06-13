using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpgrade : Upgrade {
    [Header("Specific")]
    [SerializeField] private float radius;
    private int[] burnDamageByLevel = {2, 5, 8, 10, 15};
    private float[] tickRateByLevel = {3.0f, 2.6f, 2.4f, 2.2f, 2.0f};
    private int[] tickCountByLevel = {3, 3, 4, 4, 5};

    private void Start() {
        this.type = Type.Fire;
        this.cooldown = new float[5] {5.0f, 4.5f, 4.0f, 3.5f, 3.0f};
    }

    public (int, float, int) GetPassiveParameters() {
        return (this.burnDamageByLevel[this.level], this.tickRateByLevel[this.level], this.tickCountByLevel[this.level]);
    }

    public override void Active() {
        if (!this.isActiveInCooldown) {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(this.ElapseActiveCooldown(this.cooldown[this.level]));
        }
    }

    public override void Passive() { 
        Debug.Log("FIRE PASSIVE");
    }

    public override void LevelUp() {
        // Attempt to upgrade level and make every upgrade status change.
        if (this.UpgradeLevel()) {
            // Add gem specific logic here.
        }
    }

    public override string GetBillboardText() {        
        string cost = "Cost: " + this.spiritCostByLevel[this.level].ToString () + " spirits";
        string passive1, passive2, passive3;

        if (this.level == 0) {
            passive1 = "Burn damage: 0 -> " + this.burnDamageByLevel[this.level].ToString() + "\n";
            passive2 = "Tick rate: 0 -> " + this.tickRateByLevel[this.level].ToString() + "\n";
            passive3 = "Tick count: 0 -> " + this.tickCountByLevel[this.level].ToString() + "\n";
        } else {
            passive1 = "Burn damage: " + this.burnDamageByLevel[this.level-1].ToString() + " -> " + this.burnDamageByLevel[this.level].ToString() + "\n";
            passive2 = "Tick rate: " + this.tickRateByLevel[this.level-1].ToString() + " -> " + this.tickRateByLevel[this.level].ToString() + "\n";
            passive3 = "Tick count: " + this.tickCountByLevel[this.level-1].ToString() + " -> " + this.tickCountByLevel[this.level].ToString() + "\n";
        }

        return passive1 + passive2 + passive3 + cost;
    }
}
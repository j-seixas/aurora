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

    public override string GetBillboardText() {
        string passive1, passive2;

        if (this.level == 0) {
            passive1 = "Slow amount: 0 -> " + this.slowByLevel[this.level].ToString() + "\n";
            passive2 = "Slow duration: 0 -> " + this.durationByLevel[this.level].ToString() + "\n";
        } else {
            passive1 = "Slow amount: " + this.slowByLevel[this.level-1].ToString() + " -> " + this.slowByLevel[this.level].ToString() + "\n";
            passive2 = "Slow duration: " + this.durationByLevel[this.level-1].ToString() + " -> " + this.durationByLevel[this.level].ToString() + "\n";
        }

        string cost = "Cost: " + this.spiritCostByLevel[this.level].ToString () + " spirits";
        return passive1 + passive2 + cost;
    }
}

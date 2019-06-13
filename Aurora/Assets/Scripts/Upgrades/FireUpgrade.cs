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
            AudioManager.Instance.PlaySFX("fire_ring");
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
}
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
    }

    public (int, float, int) GetPassiveParameters() {
        return (this.burnDamageByLevel[this.level], this.tickRateByLevel[this.level], this.tickCountByLevel[this.level]);
    }

    public override void Active() {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
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
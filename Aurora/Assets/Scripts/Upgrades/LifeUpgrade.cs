using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade {
    [Header("Specific")]
    public int health;
    public GameObject shieldChargeObject;

    private void Update() {
        if (!IsInvoking("Passive") && this.level > 0)
            InvokeRepeating("Passive", this.tick, this.tick);
    }

    public override void Active() {
        Instantiate(shieldChargeObject, transform.position, Quaternion.identity);
    }

    public override void Passive() {
        transform.parent.gameObject.GetComponent<PlayerController>().AddHealth(this.health);
    }

    public override void LevelUp() {
        this.level++;
        
        if (this.level != 1)
            this.health += 1;

    } 

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour {
    [SerializeField] protected float cooldown;
    [SerializeField] protected float duration;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected int damageDealt;

    public abstract void Run();

    public void ConsumeStamina() { 
        GameObject.FindWithTag("Canvas").GetComponent<HUDUpdater>().UpdateSlider("StaminaUI", -this.staminaCost);
    }
    
}

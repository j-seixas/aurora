using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour {
    [Header("Status")]
    [SerializeField] protected bool active = false;
    [SerializeField] protected int level = 0;

    [Header("Mechanics")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float duration;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected int damageDealt;

    public abstract void Active();
    public abstract void Passive();

    public void ConsumeStamina() { 
        GameObject.FindWithTag("Canvas").GetComponent<HUDUpdater>().UpdateSlider("StaminaUI", -this.staminaCost);
    }

    public int GetLevel() => 
        this.level;

    public void SetActive(bool state) =>
        this.active = state;

    public void LevelUpUpgrade() => 
        this.level++;
    
}

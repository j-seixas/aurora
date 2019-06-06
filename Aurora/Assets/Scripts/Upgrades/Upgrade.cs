using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour {
    
    public enum Type {Fire, Ice, Life}

    [Header("Status")]
    protected Type type;
    [SerializeField] protected bool active = false;
    [SerializeField] protected int level = 0;

    // The spirit cost of levelling up an upgrade.
    protected int[] spiritCostByLevel = {5, 10, 20, 50, 100};

    [Header("Mechanics")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float duration;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected int damageDealt;

    [Header("Passive")]
    [SerializeField] protected float tick;

    public abstract void Active();
    public abstract void Passive();
    public abstract void LevelUp();

    public int GetLevel() => 
        this.level;

    public void SetActive(bool state) =>
        this.active = state;

    public void UpdateHUDElements() =>
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDUpdater>().LevelUpgradeElement(this.type, this.level);
}

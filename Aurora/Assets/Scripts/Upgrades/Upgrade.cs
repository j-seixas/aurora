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

    public bool IsActiveEnabled() =>
        this.level > 0;

    public void PrintStatusPopup() {
        string text = type.ToString() + " upgrade costs " + this.spiritCostByLevel[this.level].ToString() + " spirits!";
        Debug.Log(text);
    }

    public void SetActive(bool state) =>
        this.active = state;

    protected void UpgradeLevel() {
        this.level++;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateAttribute(GameManager.Attributes.Spirits, -this.spiritCostByLevel[this.level - 1]);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDUpdater>().LevelUpgradeElement(this.type, this.level);
    }
}

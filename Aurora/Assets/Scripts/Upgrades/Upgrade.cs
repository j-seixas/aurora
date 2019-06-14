using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour {

    public enum Type { Fire, Ice, Life }

    [Header ("Status")]
    protected Type type;
    [SerializeField] protected bool active = false;
    [SerializeField] protected int level = 0;

    // The spirit cost of levelling up an upgrade.
    protected int[] spiritCostByLevel = { 5, 10, 20, 50, 100 };

    [Header ("Mechanics")]
    [SerializeField] protected float[] cooldown;
    [SerializeField] protected float duration;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected int damageDealt;

    [Header ("Active")]
    protected bool isActiveInCooldown = false;

    [Header ("Passive")]
    [SerializeField] protected float tick;

    public abstract void Active ();
    public abstract void Passive ();
    public abstract void LevelUp ();
    public abstract string GetBillboardText();

    public int GetLevel () =>
        this.level;

    public bool IsActiveEnabled () =>
        this.level > 0;

    public void PrintStatusPopup (bool active) {

        GameObject gem = GameObject.Find (type.ToString () + "GemSP").gameObject;

        foreach (RectTransform item in gem.GetComponentsInChildren<RectTransform> (true)) {
            if (item.name == "Billboard") {
                TextMesh billboard = item.gameObject.GetComponentsInChildren<TextMesh> (true) [0];
                billboard.text = type.ToString ().ToUpper () + " UPGRADE\n" + "Cost: " + this.spiritCostByLevel[this.level].ToString () + " spirits";
                item.gameObject.SetActive (active);
            }
        }
    }

    public void SetActive (bool state) {
        this.active = state;
        GameObject.FindGameObjectWithTag ("Canvas").GetComponent<HUDUpdater> ().UpdatePowerUp (this.type, this.active);

        if (this.type == Type.Fire) {
            if (state) {
                GameObject.Find("ScytheFireParticleEffect").GetComponent<ParticleSystem>().Play();
            } else {
                GameObject.Find("ScytheFireParticleEffect").GetComponent<ParticleSystem>().Stop();
            }
        }

        if (this.type == Type.Ice) {
            if (state) {
                GameObject.Find("ScytheSnowParticleEffect").GetComponent<ParticleSystem>().Play();
            } else {
                GameObject.Find("ScytheSnowParticleEffect").GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    protected IEnumerator ElapseActiveCooldown (float cooldown) {
        this.isActiveInCooldown = true;
        yield return new WaitForSeconds (cooldown);
        this.isActiveInCooldown = false;
    }

    protected bool UpgradeLevel () {
        // Does the player have enough spirits to purchase the upgrade?
        int balance = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().GetAttribute (GameManager.Attributes.Spirits);

        if (balance < this.spiritCostByLevel[this.level]) {
            Debug.Log ("Not enough balance!");
            return false;
        }

        this.level++;

        AudioManager.Instance.PlaySFX("upgrade"); //upgrade sound effect

        // Decrement the spirits and update the HUD accordingly.
        GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().UpdateAttribute (GameManager.Attributes.Spirits, -this.spiritCostByLevel[this.level - 1]);
        GameObject.FindGameObjectWithTag ("Canvas").GetComponent<HUDUpdater> ().LevelUpgradeElement (this.type, this.level);
        return true;
    }

    public bool GetActiveCooldownStatus () => !this.isActiveInCooldown;
}
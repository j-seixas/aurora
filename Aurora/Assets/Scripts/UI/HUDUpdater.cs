using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour {

    public void UpdateSlider(string type, int newValue) {
        if (type == "Health") {
            Text txtObj = GameObject.Find("HealthValue").GetComponent<Text>();
            txtObj.text = newValue.ToString() + "%";
            GameObject.Find("HealthSlider").GetComponent<Slider>().value = newValue;
        } else if (type == "Stamina") {
            GameObject.Find("StaminaSlider").GetComponent<Slider>().value = newValue;
        } else if (type == "Essence") {
            Text txtObj = GameObject.Find("SpiritsValue").GetComponent<Text>();
            txtObj.text = newValue.ToString();
        }
    }

    public void LevelUpgradeElement(Upgrade.Type type, int value) {
        Text text = GameObject.Find(type.ToString() + "PowerUp/Level").GetComponent<Text>();
        text.text = value.ToString();
    }

    public void UpdatePowerUp(Upgrade.Type type, bool active) {
        if(active)
            GameObject.Find(type.ToString() + "PowerUp/Image").GetComponent<Image>().color = new Color32(255,255,255,255);
        else GameObject.Find(type.ToString() + "PowerUp/Image").GetComponent<Image>().color = new Color32(140,140,140,255);
    }
}

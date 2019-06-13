using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour {

    public void UpdateSlider (string type, int newValue) {
        if (type == "Health") {
            Text txtObj = GameObject.Find ("HealthValue").GetComponent<Text> ();
            txtObj.text = newValue.ToString () + "%";
            GameObject.Find ("HealthSlider").GetComponent<Slider> ().value = newValue;
        } else if (type == "Stamina") {
            GameObject.Find ("StaminaSlider").GetComponent<Slider> ().value = newValue;
        } else if (type == "Essence") {
            Text txtObj = GameObject.Find ("SpiritsValue").GetComponent<Text> ();
            txtObj.text = newValue.ToString ();
        }
    }

    public void LevelUpgradeElement (Upgrade.Type type, int value) {
        Text text = GameObject.Find (type.ToString () + "PowerUp/Level").GetComponent<Text> ();
        text.text = value.ToString ();
    }

    public void UpdateCooldownStatus () {
        GameObject player = GameObject.FindGameObjectWithTag ("Player");
        Image[] images = null;

        if (player.GetComponentInChildren<FireUpgrade> ().GetActiveCooldownStatus ()) {
            images = GameObject.Find ("FirePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, true);
        } else {
            images = GameObject.Find ("FirePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, false);
        }

        if (player.GetComponentInChildren<IceUpgrade> ().GetActiveCooldownStatus ()) {
            images = GameObject.Find ("IcePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, true);
        } else {
            images = GameObject.Find ("IcePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, false);
        }

        if (player.GetComponentInChildren<LifeUpgrade> ().GetActiveCooldownStatus ()) {
            images = GameObject.Find ("LifePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, true);
        } else {
            images = GameObject.Find ("LifePowerUp").GetComponent<RectTransform> ().gameObject.GetComponentsInChildren<Image> (true);
            SwitchUpgradeStatus (images, false);
        }
    }

    private void SwitchUpgradeStatus (Image[] images, bool active) {
        foreach (Image image in images) {
            if (image.name == "Inactive") {
                if (active)
                    image.gameObject.SetActive (false);
                else image.gameObject.SetActive (true);
            } else if (image.name == "Active") {
                if (active)
                    image.gameObject.SetActive (true);
                else image.gameObject.SetActive (false);
            }
        }
    }

    public void UpdatePowerUp (Upgrade.Type type, bool active) {
        if (active) {
            GameObject.Find (type.ToString () + "PowerUp/Image").GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
            RectTransform[] canvasChildren = GameObject.Find ("HUDCanvas").GetComponent<Canvas> ().gameObject.GetComponentsInChildren<RectTransform> (true);
            foreach (RectTransform ui in canvasChildren) {
                if (ui.gameObject.name == "AbilityUI") {
                    RectTransform panel = ui;
                    StartCoroutine (ShowAbilityPanel (panel, type.ToString ()));
                }
            }
        } else GameObject.Find (type.ToString () + "PowerUp/Image").GetComponent<Image> ().color = new Color32 (140, 140, 140, 255);
    }

    private IEnumerator ShowAbilityPanel (RectTransform panel, string type) {
        Text[] words = panel.gameObject.GetComponentsInChildren<Text> (true);
        Image background = panel.gameObject.GetComponentInChildren<Image> ();
        string passive = "";
        string active = "";

        switch (type) {
            case "Fire":
                passive = "- Passive -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                active = "- Active -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                break;
            case "Ice":
                passive = "- Passive -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                active = "- Active -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                break;
            case "Life":
                passive = "- Passive -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                active = "- Active -\nLorem ipsum dolor sit amet, consectetur adipiscing elit.";
                break;
        }

        foreach (Text text in words) {
            if (text.name == "Subtitle")
                text.text = type.ToString () + " Ability";
            else if (text.name == "Content")
                text.text = passive + "\n" + active;
        }

        panel.gameObject.SetActive (true);
        StartCoroutine (FadeIn (background, words));
        yield return new WaitForSeconds (6);
        panel.gameObject.SetActive (false);
    }

    private IEnumerator FadeIn (Image background, Text[] words) {
        background.color = new Color (background.color.r, background.color.g, background.color.b, 0);
        words[0].color = new Color (words[0].color.r, words[0].color.g, words[0].color.b, 0);
        while (background.color.a < 1.0f) {
            background.color = new Color (background.color.r, background.color.g, background.color.b, background.color.a + (Time.deltaTime / 1f));

            Color c = new Color (words[0].color.r, words[0].color.g, words[0].color.b, words[0].color.a + (Time.deltaTime / 1f));
            foreach (Text text in words) {
                text.color = c;
            }
            yield return null;
        }
    }
}
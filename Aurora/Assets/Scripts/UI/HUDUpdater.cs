using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour {

    public void UpdateSlider(string type, float delta) {
        // Update text below the slider.
        Text txtObj = GameObject.Find(type + "/Value").GetComponent<Text>();
        txtObj.text = (float.Parse(txtObj.text) + delta).ToString();

        // Update slider bar.
        GameObject.Find(type + "/Slider").GetComponent<Slider>().value += delta;
    }
}

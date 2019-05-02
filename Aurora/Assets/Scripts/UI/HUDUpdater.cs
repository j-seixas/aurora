using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour {
    void Start() {   
    }

    void Update() {
        GameObject.Find("EssenceUI/Slider").GetComponent<Slider>().value = float.Parse(gameObject.GetComponent<Text>().text) + 15;
    }
}

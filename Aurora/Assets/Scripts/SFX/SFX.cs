using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {
    public AudioSource earthquake;

    public void playEarthQuake () {
        earthquake.Play ();
    }

    public void onTriggerEnter(){
        earthquake.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Storyline : MonoBehaviour {

    void Update () {
        if (Input.GetButtonDown ("Accept")) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }
    }
}
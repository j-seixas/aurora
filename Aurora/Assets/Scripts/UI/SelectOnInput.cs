using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour {
    public EventSystem eventSystem;
    public GameObject selectedObject;

    void Start () {
        Text[] children = selectedObject.GetComponentsInChildren<Text> (true);

        for (int i = 0; i < children.Length; i++) {
            if (children[i].name == "ArrowText")
                children[i].gameObject.SetActive (true);

        }
    }

    void Update () {
        if (Input.GetAxisRaw ("Vertical") != 1) {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("MenuPointer")) {
                obj.SetActive (false);
            }

            selectedObject = eventSystem.currentSelectedGameObject;
            Text[] children = selectedObject.GetComponentsInChildren<Text> (true);

            for (int i = 0; i < children.Length; i++) {
                if (children[i].name == "ArrowText")
                    children[i].gameObject.SetActive (true);
            }
        }

        if (Input.GetButtonDown ("Submit")) {
            if (selectedObject.name == "QuitButton")
                Application.Quit ();
            else if (selectedObject.name == "StartButton") {
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
            }
        }
    }
}
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

        PlayMenuSound();

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
                if (children[i].name == "ArrowText"){
                    children[i].gameObject.SetActive (true);
                }       
            }

        }

        if (Input.GetButtonDown ("Submit")) {
            PlaySelectionSound();
            if (selectedObject.name == "QuitButton")
                Application.Quit ();
            else if (selectedObject.name == "StartButton") {
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
            } else if (selectedObject.name == "QuitGameButton") {
                SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

        if(Input.GetKeyDown("up") || Input.GetKeyDown("down")){
            PlayNavigationSound();
        }
    }

    public void setSelected () {
        eventSystem.SetSelectedGameObject (selectedObject);
    }

     public void PlayNavigationSound() {
        AudioManager.Instance.PlaySFX("menu_nav");
    }

    public void PlaySelectionSound() {
        AudioManager.Instance.PlaySFX("menu_selection");
    }

    public void PlayMenuSound() {
        AudioManager.Instance.PlayMusic("MenuMusic");
        Debug.Log("Playing music");
    }
}
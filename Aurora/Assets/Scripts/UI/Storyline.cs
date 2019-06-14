using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Storyline : MonoBehaviour {

    void Start(){
        StartCoroutine(LoadGameAsync());
    }


    void Update () {
        if (Input.GetButtonDown ("Jump")) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }
    }

    IEnumerator LoadGameAsync () {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void PlayMainTheme() {
        AudioManager.Instance.PlayMusic("MainTheme");
    }
}
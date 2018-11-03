using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chooseLevel : MonoBehaviour {

    public int level;
    public GameObject loadingScreen;
    public Slider slider;

    public void loadEasy()
    {
        level = 0;
        StartCoroutine(loadAsync());
    }

    public void loadHard()
    {
        level = 1;
        StartCoroutine(loadAsync());
    }

    IEnumerator loadAsync()
    {
        PlayerPrefs.SetInt("level", level);
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void home()
    {
        SceneManager.LoadScene("mainMenu");
    }

}

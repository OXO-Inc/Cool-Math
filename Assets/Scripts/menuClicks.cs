using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Net;
using System.IO;

public class menuClicks : MonoBehaviour {

	public GameObject loadingScreen;
	public GameObject option;
	public GameObject click;
    public GameObject more;

	public Slider slider;
	public Slider volumeSlider;

	public static bool tutorial;

	private bool isOpen=false;
	private int t;

	void Start()
	{
		volumeSlider.value = PlayerPrefs.HasKey ("volumeLevel") ? PlayerPrefs.GetFloat ("volumeLevel") : 0.5f;
		t = PlayerPrefs.HasKey ("tutorial") ? PlayerPrefs.GetInt("tutorial") : 1;
		if (t == 0) {
			tutorial = false;
			StartCoroutine (panelFadeIn.FadeOut (click, .25f));
		} else
			tutorial = true;

	}

    void Update()
    {
             
    }

	public void loadScene (int sceneIndex)
	{
		StartCoroutine (loadAsync(sceneIndex));
	}

	IEnumerator loadAsync (int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);
		loadingScreen.SetActive (true);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			slider.value = progress;  
			yield return null;
		}
	} 

	public void quit() {
			Application.Quit();

	}

	public void options() {

		if (isOpen) {
			StartCoroutine (panelFadeIn.FadeOut (option, .25f));
			option.SetActive (false);
			isOpen = false;
		} else {
			isOpen = true;
			StartCoroutine (panelFadeIn.FadeTo (option, .25f));
			option.SetActive (true);
		}

	}

	public void tutorialToggle() {

		if (tutorial) {
			StartCoroutine (panelFadeIn.FadeOut (click, .25f));
			tutorial = false;
			PlayerPrefs.SetInt ("tutorial",0);

		} else {
			tutorial = true;
			StartCoroutine (panelFadeIn.FadeTo (click, .25f));
			PlayerPrefs.SetInt ("tutorial",1);
		}
			
		
	}

	public void setVolume(float volume)
	{
		//audioMixer.SetFloat ("volume",volume);
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat ("volumeLevel",AudioListener.volume);
			
	}

    public void moreGames()
    {
        Application.OpenURL("market://details?id=com.paulitevox.fabybird");
    }

    public void rate()
    {
        Application.OpenURL("market://details?id=coolmath.funlearn.kaushalag");
    }

    private string checkInternet(string resource)
    {        
        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chooseTable : MonoBehaviour {

	public string tableOf;
    public GameObject loadingScreen;
    public Slider slider;

    private RaycastHit2D hit;

	void Start () {
		
	}

	void Update () {

		if (Input.GetMouseButtonDown (0)){
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				tableOf = hit.transform.gameObject.tag;
				PlayerPrefs.SetString ("tableOf",tableOf);
                StartCoroutine(loadAsync(4));

            }
		
	}
	}

    IEnumerator loadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
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
		SceneManager.LoadScene ("mainMenu");
	}

}

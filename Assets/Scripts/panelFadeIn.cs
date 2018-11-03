using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelFadeIn : MonoBehaviour {

	public static IEnumerator FadeTo(GameObject panel,float aTime)
	{
		float alpha = panel.GetComponent<Image>().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,1f,t));
			panel.GetComponent<Image>().color = newColor;
			yield return null;
		}
		
	}

	public static IEnumerator FadeOut(GameObject panel,float aTime)
	{
		float alpha = panel.GetComponent<Image>().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,0f,t));
			panel.GetComponent<Image>().color = newColor;
			yield return null;
		}

	}

	public static IEnumerator FadeIn(GameObject sphere, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1,Mathf.Lerp(1f,0f,t) , Mathf.Lerp(1f,0f,t), 1f);
			sphere.GetComponent<Renderer>().material.color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeInBlack(GameObject sphere, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(1f,0f,t),Mathf.Lerp(1f,0f,t) , Mathf.Lerp(1f,0f,t), 1f);
			sphere.GetComponent<Renderer>().material.color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeInWhite(GameObject sphere, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(0f,1f,t),Mathf.Lerp(0f,1f,t) , Mathf.Lerp(0f,1f,t), 1f);
			sphere.GetComponentInChildren<TextMesh>().color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeInBrick(GameObject brick, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1f,1f,1f, Mathf.Lerp(0f,1f,t));
			brick.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeInText(GameObject brick, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1f,1f,1f, Mathf.Lerp(0f,1f,t));
			brick.GetComponentInChildren<TextMesh>().color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeOutBlack(GameObject brick, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(1f,0f,t),Mathf.Lerp(1f,0f,t),Mathf.Lerp(1f,0f,t),1f);
			brick.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeInOrignal(GameObject brick, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(0f,1f,t),Mathf.Lerp(0f,1f,t),Mathf.Lerp(0f,1f,t),1f);
			brick.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}

	public static IEnumerator FadeOutOrignal(GameObject brick, float aTime)
	{
		//Color c = sphere.GetComponent<Renderer>().material.color;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(0f,1f,t),Mathf.Lerp(0f,1f,t),Mathf.Lerp(0f,1f,t),Mathf.Lerp(1f,0f,t));
			brick.GetComponent<SpriteRenderer>().color = newColor;
			yield return new WaitForSeconds (.75f);
			//brick.SetActive (false);
		}
	}

}

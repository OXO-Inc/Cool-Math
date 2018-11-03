using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class handleClicks : MonoBehaviour {

	private GameObject hitObj;
	private AudioSource audio;
    private RaycastHit hit;

	private int playerSum;
	private int computerSum;
	private int playerNumbers;
	private int computerNumbers;
	private int random;
	private int forRandom;
	private int selected = 0;
	private int c_selected = 0;
	private int c;
	private int d;

	private List<int> playerNum = new List<int>();
	private List<int> computerNum = new List<int>();

	private bool checkPlayer;
	private bool checkComputer;
	private bool gameOver=false;
	private bool one;
	private bool two;
	private bool three;
	private bool four;
	private bool five;
	private bool six;
	private bool seven;
	private bool eight;
	private bool nine;
	private bool computerHasPlayed = false;
	private bool iwOpen=true; 
	private bool isPlay=true;

	public GameObject one_1;
	public GameObject two_2;
	public GameObject three_3;
	public GameObject four_4;
	public GameObject five_5;
	public GameObject six_6;
	public GameObject seven_7;
	public GameObject eight_8;
	public GameObject nine_9;

	public GameObject instruction;
	public GameObject yourTurn;
	public GameObject computersTurn;
	public GameObject youWin;
	public GameObject computerWin;
	public GameObject tied;
	public GameObject playerClick;
	public GameObject computerClick;
	public GameObject musicOn;
	public GameObject musicOff;
    public GameObject dirLight;

	public Text scorePlayer;
	public Text scoreComputer;

	public AudioSource winAudio;
	public AudioSource loseAudio;

		// Use this for initialization
	void Start () {
		
		audio = GetComponent<AudioSource>();
		if (menuClicks.tutorial) {
			instruction.SetActive (true);
			StartCoroutine (panelFadeIn.FadeTo (instruction, .5f));
		}
		else
			iwOpen = false;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (selected == 0)
			yourTurn.SetActive (true);

        Debug.Log(checkComputer);
        Debug.Log(checkPlayer);
        Debug.Log(gameOver);
        Debug.Log(selected + c_selected);

        if(!gameOver)
        {
            checkPlayer = check15(playerNum, true);
            if (checkPlayer)
            {
                gameOver = true;
                StartCoroutine(AudioFadeOut.FadeOut(audio, 2.0f));
                StartCoroutine(panelFadeIn.FadeTo(youWin, 1.5f));
                StartCoroutine(playerWon());
            }
        }

        if(!gameOver)
        {
            checkComputer = check15(computerNum, false);
            if (checkComputer)
            {
                gameOver = true;
                StartCoroutine(AudioFadeOut.FadeOut(audio, 2.0f));
                StartCoroutine(panelFadeIn.FadeTo(computerWin, 1.5f));
                StartCoroutine(computerWon());
            }
        }

        if ((selected + c_selected) == 9 && checkPlayer == false && checkComputer == false && gameOver == false)
        {
            gameOver = true;
            StartCoroutine(AudioFadeOut.FadeOut(audio, 2.0f));
            StartCoroutine(panelFadeIn.FadeTo(tied, 1.5f));
            StartCoroutine(tiedT());
        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && !gameOver && computerHasPlayed==false && !iwOpen) {
			
			hitObj = hit.transform.gameObject;
            hitObj.GetComponent<Animator>().speed = 0f;
            Handheld.Vibrate();
            yourTurn.SetActive(false);
            StartCoroutine(panelFadeIn.FadeIn(hitObj, .75f));
            StartCoroutine(panelFadeIn.FadeInWhite(hitObj, .75f));
            StartCoroutine(dispComp());
            computerHasPlayed = true;
            hit.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
            selected = selected + 1;

            if (isPlay)
                  playerClick.GetComponent<AudioSource>().Play();

            if (hit.transform.name == "one" && one == false) {
                Debug.Log(one);
				one = true;
				playerSum = playerSum + 1;
				playerNum.Add (1);                
            }
			else if (hit.transform.name == "two" && two == false) {
				
				two = true;
				playerSum = playerSum + 2;
				playerNum.Add (2);		
			}
			else if (hit.transform.name == "three" && three == false) {
	
				three = true;
				playerSum = playerSum + 3;
				playerNum.Add (3);				
			}
			else if (hit.transform.name == "four" && four == false) {
				
				four = true;
				playerSum = playerSum + 4;
				playerNum.Add (4);			
			}
			else if (hit.transform.name == "five" && five == false) {
			
				five = true;
				playerSum = playerSum + 5;
				playerNum.Add (5);		
			}
			else if (hit.transform.name == "six" && six == false) {
		
				six = true;
				playerSum = playerSum + 6;
				playerNum.Add (6);			
			}
			else if (hit.transform.name == "seven" && seven == false) {
			
				seven = true;
				playerSum = playerSum + 7;
				playerNum.Add (7);			
			}
			else if (hit.transform.name == "eight" && eight == false) {
			
				eight = true;
				playerSum = playerSum + 8;
				playerNum.Add (8);		
			}
			else if (hit.transform.name == "nine" && nine == false) {
			
				nine = true;
				playerSum = playerSum + 9;
				playerNum.Add (9);
			}

		}
	}

	IEnumerator dispComp()
	{

		yield return new WaitForSeconds(0.25f);
		if(gameOver==false)
		computersTurn.SetActive(true);
		StartCoroutine(disp(1.0f));

	}

	void ComputerPlay()
	{		
		computerHasPlayed = false;
		c = 0;
		d = 0;
		while(computerHasPlayed != true && (selected+c_selected) < 9)
		{
			forRandom = 15 - playerSum;

			if (forRandom < 10 && c == 0 && selected < 3 && selected > 1) {
					random = forRandom;
					c = 1;
				} else if (c_selected == 2 && d == 0 && 15 - computerSum < 10) {
					random = 15 - computerSum;
					d = 1;
				} else
					random = Random.Range (1, 10);
			
			
			if (random == 1 && one == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				one = true;
                one_1.GetComponent<Animator>().speed = 0f;
                one_1.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (one_1, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (one_1, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (1);
				computerSum = computerSum + 1;
				computerHasPlayed = true;
			} else if (random == 2 && two == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				two = true;
                two_2.GetComponent<Animator>().speed = 0f;
                two_2.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (two_2, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (two_2, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (2);
				computerSum = computerSum + 2;
				computerHasPlayed = true;
			}
			else if (random == 3 && three == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				three = true;
                three_3.GetComponent<Animator>().speed = 0f;
                three_3.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (three_3, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (three_3, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (3);
				computerSum = computerSum + 3;
				computerHasPlayed = true;
			}
			else if (random == 4 && four == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				four = true;
                four_4.GetComponent<Animator>().speed = 0f;
                four_4.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (four_4, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (four_4, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (4);
				computerSum = computerSum + 4;
				computerHasPlayed = true;
			}
			else if (random == 5 && five == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				five = true;
                five_5.GetComponent<Animator>().speed = 0f;
                five_5.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (five_5, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (five_5, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (5);
				computerSum = computerSum + 5;
				computerHasPlayed = true;
			}
			else if (random == 6 && six == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				six = true;
                six_6.GetComponent<Animator>().speed = 0f;
                six_6.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (six_6, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (six_6, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (6);
				computerSum = computerSum + 6;
				computerHasPlayed = true;
			}
			else if (random == 7 && seven == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				seven = true;
                seven_7.GetComponent<Animator>().speed = 0f;
                seven_7.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (seven_7, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (seven_7, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (7);
				computerSum = computerSum + 7;
				computerHasPlayed = true;
			}
			else if (random == 8 && eight == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				eight = true;
                eight_8.GetComponent<Animator>().speed = 0f;
                eight_8.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (eight_8, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (eight_8, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (8);
				computerSum = computerSum + 8;
				computerHasPlayed = true;
			}
			else if (random == 9 && nine == false) {
				if(isPlay)
					computerClick.GetComponent<AudioSource>().Play ();
				nine = true;
                nine_9.GetComponent<Animator>().speed = 0f;
                nine_9.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine (panelFadeIn.FadeInBlack (nine_9, .75f));
				StartCoroutine (panelFadeIn.FadeInWhite (nine_9, .75f));
				c_selected = c_selected + 1;
				computerNum.Add (9);
				computerSum = computerSum + 9;
				computerHasPlayed = true;
			}
		}

		StartCoroutine (dispPlayer());
        
        computerHasPlayed = false;

	}

	IEnumerator disp(float a)
	{
		
		yield return new WaitForSeconds(a);
		computersTurn.SetActive (false);
		ComputerPlay ();

	}

	IEnumerator dispPlayer()
	{

		yield return new WaitForSeconds(0.5f);
		if(gameOver==false)
		yourTurn.SetActive (true);

	}

	public void instructionWindow()
	{
		if (!iwOpen) {
			iwOpen = true;
			instruction.SetActive (true);
			StartCoroutine (panelFadeIn.FadeTo (instruction, .25f));
		} else {
			iwOpen = false;
			StartCoroutine (panelFadeIn.FadeOut (instruction, .25f));
			instruction.SetActive (false);
		}

	}

	public void songPlaying()
	{
		if (isPlay) {
			isPlay = false;
			audio.Pause ();
			musicOn.SetActive (false);
			musicOff.SetActive (true);
		} else {
			isPlay = true;
			audio.Play ();
			musicOn.SetActive (true);
			musicOff.SetActive (false);

		}

	}

	private bool check15(List<int> x, bool who)
	{
		int[] r = x.ToArray ();
		int l = r.Length;
		// Fix the first element as A[i]
		for (int i = 0; i < l-2; i++)
		{
			// Fix the second element as A[j]
			for (int j = i+1; j < l-1; j++)
			{
				// Now look for the third number
				for (int k = j+1; k < l; k++)
				{
					if (r[i] + r[j] + r[k] == 15)
					{
                        if(who)
						scorePlayer.text = r [i].ToString () + " + " + r [j].ToString () + " + " + r [k].ToString () + " = 15";
                        else
						scoreComputer.text = r [i].ToString () + " + " + r [j].ToString () + " + " + r [k].ToString () + " = 15";
						return true;
					}
				}
			}
		}

		return false;
	}

	IEnumerator playerWon()
	{
		if (isPlay)
			winAudio.Play ();
		yourTurn.SetActive (false);
		computersTurn.SetActive (false);
		yield return new WaitForSeconds(1.25f);
		youWin.SetActive (true);
	}

	IEnumerator computerWon()
	{
		if (isPlay)
			loseAudio.Play ();
		yourTurn.SetActive (false);
		computersTurn.SetActive (false);
		yield return new WaitForSeconds(1.25f);
		computerWin.SetActive (true);
	}

	IEnumerator tiedT()
	{
		yourTurn.SetActive (false);
		computersTurn.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		tied.SetActive (true);
	}

	public void restart()
	{
		SceneManager.LoadScene ("make15");
	}

	public void home()
	{
		SceneManager.LoadScene ("mainMenu");
	}

}

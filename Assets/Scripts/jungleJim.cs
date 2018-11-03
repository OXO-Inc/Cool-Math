using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class jungleJim : MonoBehaviour {

	static System.Random random = new System.Random();

	private bool gameOver = false;
    private bool isPlay = true;
    private bool iwOpen = true;

	private string selected;
	private string tableOf;

	private int selectedToInt;
	private int tableOff;
	private int randomNumber;
	private int rn;
	private int rn1;
	private int rn2;
	private int rn3;
	private int val;
	private int correct;
	private int sc;
	private int sm;
    private int temp;
    private int s;
    private int sp;

	private float secondCount=-1;
    private float ts;
    private float te;

    private Vector2 r1s = new Vector2(0f, 3.4f);
    private Vector2 r1e = new Vector2(0f, 1.85f);
    private Vector2 r2s = new Vector2(3.85f, 1f);
    private Vector2 r2e = new Vector2(1.8f, -0.9f);
    private Vector2 r3s = new Vector2(-4f, -3.45f);
    private Vector2 r3e = new Vector2(-2.9f, -1.5f);
    private Vector2 r4s = new Vector2(-6.7f, 2.8f);
    private Vector2 r4e = new Vector2(-5.5f, 0.85f);

    private RaycastHit2D hit;

	List<int> usedValues = new List<int>();
    AudioSource audio;

	public Text toMultiply;
	public Text scoreC;
	public Text scoreM;
    public Text scoreP;
    public Text scoreN;
    public Text score;
    public Text time;
    public Text scoreB;

	public GameObject r1;
	public GameObject r2;
	public GameObject r3;
	public GameObject r4;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject youWin;
    public GameObject scoreBeat;
    public GameObject arrow;
    public GameObject instruction;

    public AudioSource clap;
    public AudioSource end;

	void Start () {

        if (PlayerPrefs.HasKey("score"))
        {
            scoreB.text = (PlayerPrefs.GetInt("score")).ToString();
        }

        if (menuClicks.tutorial)
        {
            instruction.SetActive(true);
            StartCoroutine(panelFadeIn.FadeTo(instruction, .5f));
        }
        else
            iwOpen = false;

        audio = GetComponent<AudioSource>();
        tableOf = PlayerPrefs.GetString ("tableOf");
		tableOff = int.Parse (tableOf);
		generateRandom ();
	}

	void generateRandom(){
		
		rn = random.Next (8) + 1;
		correct = rn * tableOff;
		toMultiply.text = tableOf + " X " + rn.ToString ();
		randomNumber = random.Next (3) + 1;
		generateRemainingRandom (rn);
		if (randomNumber == 1) {
			r1.GetComponentInChildren<TextMesh> ().text = correct.ToString ();
			r2.GetComponentInChildren<TextMesh> ().text = (tableOff*rn1).ToString ();
			r3.GetComponentInChildren<TextMesh> ().text = (tableOff*rn2).ToString ();
			r4.GetComponentInChildren<TextMesh> ().text = (tableOff*rn3).ToString ();
		}
		else if (randomNumber == 2) {
			r1.GetComponentInChildren<TextMesh> ().text = (tableOff*rn1).ToString ();
			r2.GetComponentInChildren<TextMesh> ().text = correct.ToString ();
			r3.GetComponentInChildren<TextMesh> ().text = (tableOff*rn2).ToString ();
			r4.GetComponentInChildren<TextMesh> ().text = (tableOff*rn3).ToString ();
		}
		else if (randomNumber == 3) {
			r1.GetComponentInChildren<TextMesh> ().text = (tableOff * rn2).ToString ();
			r2.GetComponentInChildren<TextMesh> ().text = (tableOff * rn1).ToString ();
			r3.GetComponentInChildren<TextMesh> ().text = (correct).ToString ();
			r4.GetComponentInChildren<TextMesh> ().text = (tableOff * rn3).ToString ();
		} else {
			r1.GetComponentInChildren<TextMesh> ().text = (tableOff*rn3).ToString ();
			r2.GetComponentInChildren<TextMesh> ().text = (tableOff*rn2).ToString ();
			r3.GetComponentInChildren<TextMesh> ().text = (tableOff*rn1).ToString ();
			r4.GetComponentInChildren<TextMesh> ().text = correct.ToString ();
		}
	}

	void generateRemainingRandom(int a){
		
		usedValues.Add (a);
		rn1 = Random.Range(1, 9);
		while(usedValues.Contains(rn1))
		{
			rn1 = Random.Range(1, 9);
		}
		usedValues.Add (rn1);
		rn2 = Random.Range(1, 9);
		while(usedValues.Contains(rn2))
		{
			rn2 = Random.Range(1, 9);
		}
		usedValues.Add (rn2);
		rn3 = Random.Range(1, 9);
		while(usedValues.Contains(rn3))
		{
			rn3 = Random.Range(1, 9);
		}
		usedValues.Clear ();
	}

	void Update () {

        if (!gameOver)
        {
            if (string.Compare(r1.GetComponentInChildren<TextMesh>().text, "6") != 0 && string.Compare(r1.GetComponentInChildren<TextMesh>().text, "9") != 0)
                r1.transform.Rotate(0, 0, 100 * Time.deltaTime);
            else
                r1.transform.eulerAngles = new Vector2(0f, 0f);
            if (string.Compare(r2.GetComponentInChildren<TextMesh>().text, "6") != 0 && string.Compare(r2.GetComponentInChildren<TextMesh>().text, "9") != 0)
                r2.transform.Rotate(0, 0, -90 * Time.deltaTime);
            else
                r2.transform.eulerAngles = new Vector2(0f, 0f);
            if (string.Compare(r3.GetComponentInChildren<TextMesh>().text, "6") != 0 && string.Compare(r3.GetComponentInChildren<TextMesh>().text, "9") != 0)
                r3.transform.Rotate(0, 0, 110 * Time.deltaTime);
            else
                r3.transform.eulerAngles = new Vector2(0f, 0f);
            if (string.Compare(r4.GetComponentInChildren<TextMesh>().text, "6") != 0 && string.Compare(r4.GetComponentInChildren<TextMesh>().text, "9") != 0)
                r4.transform.Rotate(0, 0, -80 * Time.deltaTime); 
            else
                r4.transform.eulerAngles = new Vector2(0f, 0f);
        }

        if(gameOver)
            arrow.SetActive(false);

        if (!iwOpen)
        {
            secondCount += Time.deltaTime;
            ts += Time.deltaTime;

            if (secondCount < 50 && gameOver == false)
                time.text = "00:" + (59 - (int)secondCount).ToString();
            else if ((int)secondCount == 60 && gameOver == false)
            {
                gameOver = true;
                s = sc - sm;
                audio.Stop();
                if (!PlayerPrefs.HasKey("score"))
                {
                    PlayerPrefs.SetInt("score", s);
                    score.text = s.ToString();
                    if (isPlay)
                        end.Play();
                    StartCoroutine(win());
                }
                else
                {
                    sp = PlayerPrefs.GetInt("score");
                    if (sp >= s)
                    {
                        score.text = s.ToString();
                        if (isPlay)
                            end.Play();
                        StartCoroutine(win());
                    }
                    else
                    {
                        scoreP.text = sp.ToString();
                        scoreN.text = s.ToString();
                        PlayerPrefs.SetInt("score", s);
                        if (isPlay)
                            end.Play();
                        StartCoroutine(scoreBeaten());
                    }

                }
            }
            else if (gameOver == false)
                time.text = "00:0" + (59 - (int)secondCount).ToString();
        }

		if (Input.GetMouseButtonDown (0) && gameOver == false) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				selected = hit.transform.gameObject.GetComponentInChildren<TextMesh> ().text;
				selectedToInt = int.Parse (selected);
				if (selectedToInt == correct) {
                    if (isPlay)
                        clap.Play();
					sc += 1;
					scoreC.text = sc.ToString ();
                    temp += 2;
                    StartCoroutine(wait());
                    ts = 0;
                } else {
					sm += 1;
					scoreM.text = sm.ToString ();
                    Handheld.Vibrate();
				}

			}
		
		}

        if(temp%2==0 && temp!=0)
        {
            if (ts < 0.7f)
            {
                r1.transform.position = Vector2.Lerp(r1e, r1s, ts);
                r2.transform.position = Vector2.Lerp(r2e, r2s, ts);
                r3.transform.position = Vector2.Lerp(r3e, r3s, ts);
                r4.transform.position = Vector2.Lerp(r4e, r4s, ts);
            }
            else
            {
                r1.transform.position = Vector2.Lerp(r1.transform.position, r1e, ts);
                r2.transform.position = Vector2.Lerp(r2.transform.position, r2e, ts);
                r3.transform.position = Vector2.Lerp(r3.transform.position, r3e, ts);
                r4.transform.position = Vector2.Lerp(r4.transform.position, r4e, ts);
                
            }
        }
	}

    IEnumerator win()
    {
        StartCoroutine(panelFadeIn.FadeTo(youWin, 1.5f));
        yield return new WaitForSeconds(1.25f);
        youWin.SetActive(true);
    }

    IEnumerator scoreBeaten()
    {
        StartCoroutine(panelFadeIn.FadeTo(scoreBeat, 1.5f));
        yield return new WaitForSeconds(1.25f);
        scoreBeat.SetActive(true);
    }

    public void songPlaying()
    {
        if (isPlay)
        {
            isPlay = false;
            audio.Pause();
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            isPlay = true;
            audio.Play();
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }

    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.7f);
        generateRandom();
    }

    public void instructionWindow()
    {
        if (!iwOpen)
        {
            iwOpen = true;
            instruction.SetActive(true);
            StartCoroutine(panelFadeIn.FadeTo(instruction, .25f));
        }
        else
        {
            iwOpen = false;
            StartCoroutine(panelFadeIn.FadeOut(instruction, .25f));
            instruction.SetActive(false);
        }

    }

    public void chooseTable()
	{
		SceneManager.LoadScene ("chooseTable");
	}

    public void mainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

}

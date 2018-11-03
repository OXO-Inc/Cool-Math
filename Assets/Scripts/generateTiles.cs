using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class generateTiles : MonoBehaviour {

	static System.Random random = new System.Random();

	public GameObject prefab;
    public GameObject easyBrick;
	public GameObject musicOff;
	public GameObject musicOn;
	public GameObject win;
	public GameObject loose;
	public GameObject timeWin;
	public GameObject inf;
	public GameObject bestTime;
	public Text best;
	public Text time;
	public Text pairsToGo;
	public Text timeTaken;
	public Text preTime;
	public Text newTime;
    public Text pairs;
	public AudioSource background;
	public AudioSource tileBreak;
	public AudioSource clockTick;
	public AudioSource youWinAudio;
	public AudioSource youLoseAudio;


	private List<string> numbers1 = new List<string>{"1/2","1/3","1/4","1/5","2/5","1/6","1/7","2/7","3/7","1/8","3/8","1/9","2/9","4/9","2/4","3/9","3/6","6/8"};
	private List<string> numbers2 = new List<string>{"1/2","2/3","3/4","4/5","3/5","5/6","6/7","5/7","4/7","7/8","5/8","8/9","7/9","5/9","2/4","6/9","3/6","2/8"};

	private List<GameObject> selected = new List<GameObject>{};

	private string number;

	private int index;
	private int countSelected;
	private int correct;
	private int startingLife = 100;
	private int currentLife;
    private int level;
    private int brickCount;
    private int total;

	private float tag;
	private float minuteCount=0;
	private float secondCount=0;
	private float sum = 0;
	private float initial_x = -2.75f;
	private float initial_y = 3.9f;
    private float jumpX = 1.66f;
    private float jumpY = -1.55f;

	private RaycastHit2D hit;

	private bool isOpen = true;
	private bool spawned = false;
	private bool gameOver = false;
	private bool music = true;
	private bool playerWin;

	public GameObject instruction;
	public Slider health;

    void Awake()
    {
        level = PlayerPrefs.GetInt("level");
        if (level == 0)
        {
            brickCount = 4;
            initial_x = -2.1f;
            initial_y = 3.5f;
            prefab = easyBrick;
            jumpX = 2.24f;
            jumpY = -2.24f;
            numbers1 = new List<string> { "1/2", "1/3", "1/4", "1/5", "2/6", "4/8", "2/8", "3/9" };
            numbers2 = new List<string> { "1/2", "2/3", "3/4", "4/5", "4/6", "4/8", "6/8", "6/9" };
}
        else
            brickCount = 6;
        total = brickCount * brickCount;
        pairsToGo.text = (total / 2).ToString();
        Debug.Log(level);
    }

	IEnumerator Start () {
		if (PlayerPrefs.HasKey ("bestTime")) {
			inf.SetActive (false);
			bestTime.SetActive (true);
			best.text = PlayerPrefs.GetString ("bestTime");
		}

		for (int i = 0; i < brickCount; i++) {
			for (int height = 0; height < brickCount; height++) {
				yield return new WaitForSeconds (.05f);
				spawnBricks (i,height);
			}
		}
		if (menuClicks.tutorial) {
			instruction.SetActive (true);
			StartCoroutine (panelFadeIn.FadeTo (instruction, .3f));
		}
		else
			isOpen = false;
		spawned = true;
		currentLife = startingLife;
	}

	void spawnBricks(int i, int height)
	{
		GameObject brick = Instantiate (prefab);
		TextMesh textObject = brick.GetComponentInChildren<TextMesh> ();
		if (i % 2 == 0) {
			index = random.Next (numbers1.Count);
			number = numbers1[index];
			tag = toFloat (number);
			textObject.text = number;
			numbers1.RemoveAt (index);

		} else {
			index = random.Next (numbers2.Count);
			number = numbers2[index];
			tag = toFloat (number);
			textObject.text = number;
			numbers2.RemoveAt (index);
		}
		brick.tag = tag.ToString ();
		brick.transform.position = new Vector2 (initial_x + i * jumpX, initial_y + height *jumpY);
		//StartCoroutine (panelFadeIn.FadeInBrick (brick, .50f));
		StartCoroutine (panelFadeIn.FadeInText (brick, .50f));
	}

	float toFloat(string temp)
	{
		float a = float.Parse(temp [0].ToString ());
		float b = float.Parse (temp [2].ToString ());
		float c = a / b;
		return c;
	}
	// Update is called once per frame
	void Update () {

		if (!isOpen && !gameOver) {
			secondCount += Time.deltaTime;
			if (minuteCount < 10 && secondCount < 10)
				time.text = "0" + minuteCount.ToString () + ":0" + ((int)secondCount).ToString ();
			else if (minuteCount < 10 && secondCount > 10)
				time.text = "0" + minuteCount.ToString () + ":" + ((int)secondCount).ToString ();
			else if (minuteCount > 10 && secondCount < 10)
				time.text = minuteCount.ToString () + ":0" + ((int)secondCount).ToString ();
			else
				time.text = minuteCount.ToString () + ":" + ((int)secondCount).ToString ();

			Debug.Log (secondCount%1);
			if (music && secondCount % 1 > 0.92f)
				clockTick.Play ();
			else
				clockTick.Stop ();
				
		}

		if (secondCount >= 60 && !gameOver) {
			minuteCount++;
			secondCount = 0;
		}

		if (gameOver && !playerWin) {
			background.Stop ();
			StartCoroutine (panelFadeIn.FadeTo (loose, 2.5f));
			StartCoroutine (youLost());
		}

		if (correct == total && !gameOver) {
			playerWin = true;
			gameOver = true;
			background.Stop ();
			if(music)
			youWinAudio.Play ();
			if (!PlayerPrefs.HasKey ("bestTime")) {
				PlayerPrefs.SetFloat ("minuteCount", minuteCount);
				PlayerPrefs.SetFloat ("secondCount", secondCount);
				PlayerPrefs.SetString ("bestTime", time.text);
				StartCoroutine (youWin());
			} else {
				if (minuteCount < PlayerPrefs.GetFloat ("minuteCount")) {
					preTime.text = PlayerPrefs.GetString ("bestTime");
					PlayerPrefs.SetFloat ("minuteCount", minuteCount);
					PlayerPrefs.SetFloat ("secondCount", secondCount);
					PlayerPrefs.SetString ("bestTime", time.text);
					StartCoroutine (youTimeWin());
				} else if (minuteCount == PlayerPrefs.GetFloat ("minuteCount") && secondCount < PlayerPrefs.GetFloat ("secondCount")) {
                    preTime.text = PlayerPrefs.GetString("bestTime");
                    PlayerPrefs.GetString ("bestTime");
					PlayerPrefs.SetFloat ("secondCount", secondCount);
					PlayerPrefs.SetString ("bestTime", time.text);
					StartCoroutine (youTimeWin());
				} else
					StartCoroutine (youWin());
			}

		}

		if (countSelected == 2 && sum == 1) {
			sum = 0;
			correct += 2;
			// To-Do Animation
			selected[0].SetActive (false);
			selected [1].SetActive (false);
			selected.Clear ();
			if(music)
			tileBreak.Play ();
			countSelected = 0;
            if (total / 2 - correct / 2 < 2) {
                pairsToGo.text = (total / 2 - correct / 2).ToString();
                pairs.text = "PAIR\nTO GO";
            }
            else
			pairsToGo.text = (total/2 - correct / 2).ToString ();
		} else if (countSelected == 2 && sum != 1) {
			sum = 0;
			StartCoroutine (panelFadeIn.FadeInOrignal (selected [0], .75f));
			StartCoroutine (panelFadeIn.FadeInOrignal (selected [1], .75f));
			selected.Clear ();
			countSelected = 0;
			decreaseLife ();
			Handheld.Vibrate ();
		} 

		if (Input.GetMouseButtonDown (0) && gameOver==false && spawned){
			
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
					StartCoroutine (panelFadeIn.FadeOutBlack (hit.transform.gameObject, .75f));
					countSelected += 1;
					selected.Add (hit.transform.gameObject);
					sum += float.Parse (hit.transform.gameObject.tag);		
				
			}
		}
	}

	IEnumerator youLost()
	{
		
		yield return new WaitForSeconds(1.25f);
		loose.SetActive (true);
	}

	IEnumerator youWin()
	{
		timeTaken.text = time.text;
		StartCoroutine (panelFadeIn.FadeTo (win, 2.5f));
		yield return new WaitForSeconds(1.25f);
		win.SetActive (true);
	}

	IEnumerator youTimeWin()
	{
		newTime.text = time.text;
		StartCoroutine (panelFadeIn.FadeTo (timeWin, 2.5f));
		yield return new WaitForSeconds(1.25f);
		timeWin.SetActive (true);
	}

	private void decreaseLife()
	{
		currentLife -= 20;
		health.value = currentLife;

		if (currentLife <= 0) {
			gameOver = true;
			if(music)
			youLoseAudio.Play ();
		}
	}
		
	public void instructionWindow()
	{
		if (!isOpen) {
			isOpen = true;
			StartCoroutine (panelFadeIn.FadeTo (instruction, .30f));
			instruction.SetActive (true);
		} else {
			isOpen = false;
			StartCoroutine (panelFadeIn.FadeOut (instruction, .30f));
			instruction.SetActive (false);
		}

	}

	public void soundSelect()
	{
		if (music) {
			music = false;
			background.Stop ();
			musicOff.SetActive (true);
			musicOn.SetActive (false);
		} else {
			music = true;
			background.Play ();
			musicOn.SetActive (true);
			musicOff.SetActive (false);
		}
	}

	public void restart()
	{
		SceneManager.LoadScene ("fractone");
	}

	public void home()
	{
		SceneManager.LoadScene ("mainMenu");
	}

}

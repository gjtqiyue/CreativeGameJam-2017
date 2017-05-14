using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public bool gameActive = false;
	public bool startMenuActive = false;
    public bool endMenuActive = false;

    public GameObject nameInputField;
    public GameObject scoreCanvas;
    public GameObject gameCanvas;
    public float timeDuration;
    public int numOfBugs;
    public int remainingNumOfBugs;
	public GameObject menu;
    public CameraManager cameraManager;
    public Text textTimer;

	private bool startGame;
    private float gameTimer;
    private bool gameOverActivated;
    private bool raiseHeadTrigger;

    public GameObject leftChopstick;
    public GameObject rightChopstick;

    public const string CHOPSTICK = "Chopstick";
    public const string FOOD = "Food";

    void Awake()
    {
        SetChopsticksJoystick();
    }

    void Start()
    {
        InitializeVariables();
        EnableChopsticks(false);
        menu.GetComponentInChildren<AnimFold>().Unfold();
    }

    private void SetChopsticksJoystick()
    {
        leftChopstick.GetComponent<ChopstickControllerScript>().SetController(1);
        rightChopstick.GetComponent<ChopstickControllerScript>().SetController(2);
    }

    private void InitializeVariables()
    {
		startMenuActive = true;
        raiseHeadTrigger = false;
        gameOverActivated = false;
        timeDuration = 60;
        numOfBugs = 10;
        remainingNumOfBugs = 0;
        gameTimer = timeDuration;
		cameraManager = Camera.main.GetComponent <CameraManager> ();
        ScoreManager.Instance.InitializeScoreManager();
        textTimer.text = string.Format("{0:00.00}", "0");
    }

    private void EnableChopsticks(bool enable)
    {
        leftChopstick.GetComponent<ChopstickControllerScript>().EnableControls(enable);
        rightChopstick.GetComponent<ChopstickControllerScript>().EnableControls(enable);
    }

    void Update()
    {
        if (startMenuActive)
        {
			//start game
            if (Input.GetKeyDown("r"))
            {
                startMenuActive = false;
                // Duck head
				cameraManager.DuckCamera();
				menu.GetComponentInChildren <AnimFold> ().Fold ();
				menu.GetComponent <AnimPutdown> ().PutDown ();
            }
			if (Input.GetKeyDown ("q")) {
				Application.Quit ();
			}
        }
        if (gameActive)
        {
            textTimer.text = string.Format("{0:00.00}", gameTimer);
            gameTimer -= Time.deltaTime;
            //end condition 1: time is up
            if (!gameOverActivated && TimeIsUp())
            {
                textTimer.text = string.Format("{0:00.00}", "0");
                gameActive = false;               
                gameOverActivated = true;
                // save score
                cameraManager.RaiseCamera();
				endMenuActive = true;
                /*
			    if (Input.GetButtonDown ("Joy1ButtA") && Input.GetButtonDown ("Joy2ButtA"))
				    SceneManager.LoadScene ("Main");*/

                Debug.Log("Game Over");
            }
            //end condition 2: eat all the things
            if (!gameOverActivated && NoBugsLeft())
            {
                Debug.Log("Game over");
                textTimer.text = string.Format("{0:00.00}", "0");
                gameActive = false;
                gameOverActivated = true;
                cameraManager.RaiseCamera();
				endMenuActive = true;
                //pop up a text to say the game over
                //go to the scoreboard
                //ask restart? or quit
            }
        }
        if (endMenuActive)
        {
            if (Input.GetKeyDown("r"))
            {
                endMenuActive = false;
                // put up the menu
				menu.GetComponent <AnimPutdown> ().PutUp ();
				menu.GetComponentInChildren <AnimFold> ().Unfold();
				Debug.Log ("Restart Game");
				InitializeVariables ();
            }
        }
    }
	
    public void GetName()
    {
        gameCanvas.SetActive(false);
        scoreCanvas.transform.GetChild(0).GetComponent<Animator>().Play("ScorePanelAnimDown");
        Transform panel = scoreCanvas.transform.GetChild(0);
        Transform names = panel.transform.FindChild("Names");
        Transform scores = panel.transform.FindChild("Scores");

        int i = 0;
        foreach (Transform child in names)
        {
            if (i < ScoreSaveLoad.sortedNames.Count)
            {
                child.GetComponent<Text>().text = ScoreSaveLoad.sortedNames[i];
                i++;
            }
            else
            {
                break;
            }
        }

        i = 0;
        foreach (Transform child in scores)
        {
            if (i < ScoreSaveLoad.sortedScores.Count)
            {
                child.GetComponent<Text>().text = ScoreSaveLoad.sortedScores[i].ToString();
                i++;
            }
            else
            {
                break;
            }
        }
        panel.transform.FindChild("YourScore").GetComponent<Text>().text = ScoreManager.Instance.score.ToString();
        nameInputField.SetActive(true);
    }

	public void StartGame ()
	{
		gameActive = true;
        gameCanvas.SetActive(true);
        FoodSpawnerScript.Instance.SpawnFood(numOfBugs);
        remainingNumOfBugs = numOfBugs;
        EnableChopsticks(true);
    }
		

    public void ReStartGame()
    {
        InitializeVariables();
    }

    public void EndGame(string name)
    {
        EnableChopsticks(false);
        ManageScore(name);
        // Do end game actions
        endMenuActive = true;
    }

    public void ManageScore(string name)
    {
        if (nameInputField.GetComponent<InputField>().text.Length > 0)
        {
            Debug.Log("Text has been entered");
        }
        else if (nameInputField.GetComponent<InputField>().text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
        ScoreSaveLoad.AddScore(name, ScoreManager.Instance.score);
        ScoreSaveLoad.Save();
        ScoreSaveLoad.Sort();

        scoreCanvas.transform.GetChild(0).GetComponent<Animator>().Play("ScorePanelAnimUp");
    }
    
	public void RaiseHead ()
	{
		raiseHeadTrigger = true;
	}

	public bool getRaiseHeadTrigger(){
		return raiseHeadTrigger;
	}

	public bool TimeIsUp ()
	{
		if (gameTimer <= 0)
			return true;
		else
			return false;
	}

	public bool NoBugsLeft ()
	{
		if (remainingNumOfBugs == 0)
			return true;
		else
			return false;

	}
}

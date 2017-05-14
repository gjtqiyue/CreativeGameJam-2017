using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public bool gameActive = false;
    public bool startMenuActive = true;
    public bool endMenuActive = false;

    public GameObject nameInputField;
    public GameObject scoreCanvas;
    public GameObject gameCanvas;
    public float timeDuration;
    public int numOfBugs;
    public int remainingNumOfBugs;
	[HideInInspector]
    public ScoreManager scoreManager;
	public GameObject menu;
    public CameraManager cameraManager;
    public Text textTimer;

	private bool startGame;
    private float gameTimer;
    private bool gameOverActivated;
    private bool raiseHeadTrigger;


    void Start()
    {
        InitializeVariables();
        menu.GetComponentInChildren<AnimFold>().Unfold();
    }

    private void InitializeVariables()
    {
        raiseHeadTrigger = false;
        gameOverActivated = false;
        remainingNumOfBugs = numOfBugs;
        gameTimer = timeDuration;
		cameraManager = Camera.main.GetComponent <CameraManager> ();
        scoreManager = GetComponent<ScoreManager>();
        scoreManager.InitializeScoreManager();
        textTimer.text = string.Format("{0:00.00}", "0");

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
                /*
			    if (Input.GetButtonDown ("Joy1ButtA") && Input.GetButtonDown ("Joy2ButtA"))
				    SceneManager.LoadScene ("Main");*/

                Debug.Log("Game Over");
            }
            //end condition 2: eat all the things
            if (!gameOverActivated && NoBugsLeft())
            {
                textTimer.text = string.Format("{0:00.00}", "0");
                gameActive = false;
                gameOverActivated = true;
                RaiseHead();
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
                // Duck head


            }
        }
    }
	
    public void GetName()
    {
        gameCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        Transform panel = scoreCanvas.transform.GetChild(0);
        Transform names = panel.transform.FindChild("Names");
        Transform scores = panel.transform.FindChild("Scores");

        int i = 0;
        foreach (Transform child in names)
        {
            child.GetComponent<Text>().text = ScoreSaveLoad.sortedNames[i];
            i++;
        }

        i = 0;
        foreach (Transform child in scores)
        {
            child.GetComponent<Text>().text = ScoreSaveLoad.sortedScores[i].ToString();
            i++;
        }
        panel.transform.FindChild("YourScore").GetComponent<Text>().text = scoreManager.score.ToString();
        nameInputField.SetActive(true);
    }

	public void StartGame ()
	{
		gameActive = true;
        gameCanvas.SetActive(true);
	}
		

    public void ReStartGame()
    {
        InitializeVariables();
    }

    public void EndGame(string name)
    {
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
        ScoreSaveLoad.AddScore(name, scoreManager.score);
        ScoreSaveLoad.Save();
        ScoreSaveLoad.Sort();

        
        scoreCanvas.SetActive(false);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public GameObject nameInputField;
    public float timeDuration;
    public int numOfBugs;
    public int remainingNumOfBugs;
    [HideInInspector]
    public ScoreManager scoreManager;

    private float gameTimer;
    private bool gameOverActivated;
	public float timeDuration;
	public int numOfBugs;

    void Start()
    {
        gameOverActivated = false;
        remainingNumOfBugs = numOfBugs;
        gameTimer = timeDuration;
        scoreManager = GetComponent<ScoreManager>();
    }
	private int remainingNumOfBugs;
	private bool raiseHeadTrigger;
	private float gameTimer;

	void Start ()
	{
		raiseHeadTrigger = false;
		remainingNumOfBugs = numOfBugs;
		gameTimer = timeDuration;
	}
	

    void Update()
    {
        gameTimer -= Time.deltaTime;
        //end condition 1: time is up
        if (!gameOverActivated && gameTimer <= 0)
        {
            gameOverActivated = true;
            // save score
            GetName();
            
            Debug.Log("Game Over");
        }
        //end condition 2: eat all the things
        if (!gameOverActivated && remainingNumOfBugs == 0)
        {
            gameOverActivated = true;
            //pop up a text to say the game over
            //go to the scoreboard
            //ask restart? or quit
        }
    }
	void Update () {
		gameTimer -= Time.deltaTime;
		//end condition 1: time is up
		if (TimeIsUp ()) {
			//pop up a text to say the game over
			//go to the scoreboard
			//ask restart? or quit
			RaiseHead ();
			/*
			if (Input.GetButtonDown ("Joy1ButtA") && Input.GetButtonDown ("Joy2ButtA"))
				SceneManager.LoadScene ("Main");*/
		}
		//end condition 2: eat all the things
		if (NoBugsLeft ()) {
			//pop up a text to say the game over
			//go to the scoreboard
			//ask restart? or quit
			RaiseHead ();
		}
	}

    void GetName()
    {
        nameInputField.SetActive(true);
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

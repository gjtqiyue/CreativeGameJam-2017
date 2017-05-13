using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public bool gameActive = true;
    public bool startMenuActive = false;
    public bool endMenuActive = false;

    public GameObject nameInputField;
    public float timeDuration;
    public int numOfBugs;
    public int remainingNumOfBugs;
    [HideInInspector]
    public ScoreManager scoreManager;
    public CameraManager cameraManager;

    private float gameTimer;
    private bool gameOverActivated;
    private bool raiseHeadTrigger;


    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        raiseHeadTrigger = false;
        gameOverActivated = false;
        remainingNumOfBugs = numOfBugs;
        gameTimer = timeDuration;
        scoreManager = GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (startMenuActive)
        {
            if (Input.GetKeyDown("r"))
            {
                startMenuActive = false;
                // Duck head

            }
        }
        if (gameActive)
        {
            gameTimer -= Time.deltaTime;
            //end condition 1: time is up
            if (!gameOverActivated && TimeIsUp())
            {
                gameActive = false;               
                gameOverActivated = true;
                // save score
                RaiseHead();
                cameraManager.RaiseCamera();
                /*
			    if (Input.GetButtonDown ("Joy1ButtA") && Input.GetButtonDown ("Joy2ButtA"))
				    SceneManager.LoadScene ("Main");*/

                Debug.Log("Game Over");
            }
            //end condition 2: eat all the things
            if (!gameOverActivated && NoBugsLeft())
            {
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
        nameInputField.SetActive(true);
    }

    public void StartGame()
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

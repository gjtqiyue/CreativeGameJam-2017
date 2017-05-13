using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        gameOverActivated = false;
        remainingNumOfBugs = numOfBugs;
        gameTimer = timeDuration;
        scoreManager = GetComponent<ScoreManager>();
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
    
}

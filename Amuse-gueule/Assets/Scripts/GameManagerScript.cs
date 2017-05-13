using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
<<<<<<< HEAD

    public float timeDuration;
    public int numOfBugs;
    public int remainingNumOfBugs;

    private float gameTimer;

    void Start()
    {
        remainingNumOfBugs = numOfBugs;
        gameTimer = timeDuration;
    }


    void Update()
    {
        gameTimer -= Time.deltaTime;
        //end condition 1: time is up
        if (gameTimer <= 0)
        {
            //pop up a text to say the game over
            //go to the scoreboard
            //ask restart? or quit
        }
        //end condition 2: eat all the things
        if (remainingNumOfBugs == 0)
        {
            //pop up a text to say the game over
            //go to the scoreboard
            //ask restart? or quit
        }

    }
=======
    public ScoreManager scoreManager;

	// Use this for initialization
	void Start ()
	{
        scoreManager = GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
>>>>>>> combo
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{

	public float timeDuration;
	public int numOfBugs;
	public int remainingNumOfBugs;

	private float gameTimer;

	void Start ()
	{
		remainingNumOfBugs = numOfBugs;
		timeDuration = 10;
		gameTimer = timeDuration;
	}
	

	void Update () {
		gameTimer -= Time.deltaTime;
		//end condition 1: time is up
		if (gameTimer <= 0)
		{
			//pop up a text to say the game over
			//go to the scoreboard
			//ask restart? or quit
	}
		//end condition 2: eat all the things
		if (some function == 0)

}

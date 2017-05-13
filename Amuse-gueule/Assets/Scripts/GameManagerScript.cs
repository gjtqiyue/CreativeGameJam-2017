using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{

	public float timeDuration;
	public int numOfBugs;

	private int remainingNumOfBugs;
	private bool raiseHeadTrigger;
	private float gameTimer;

	void Start ()
	{
		raiseHeadTrigger = false;
		remainingNumOfBugs = numOfBugs;
		gameTimer = timeDuration;
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

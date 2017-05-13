using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
	private bool startGame;

	// Use this for initialization
	void Start ()
	{
		startGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.P)) {
			StartGame ();
		}
	}

	public void StartGame()
	{
		startGame = true;
	}

	public bool getStartGame (){
		return startGame;
	}
}

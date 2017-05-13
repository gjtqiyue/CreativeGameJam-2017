using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public ScoreManager scoreManager;

	// Use this for initialization
	void Start ()
	{
        scoreManager = GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

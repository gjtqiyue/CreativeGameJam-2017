using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButton ("Joy1ButtA") && Input.GetButton ("Joy2ButtA")) 
		{
			//game start
			print("Game start");
		} 
		else if (Input.GetButton ("Joy1ButtB") && Input.GetButton ("Joy2ButtB"))
		{
			//exit game
			print("Exit");
		}
		/*
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.P)) {
			//game start
			print ("Game start");
		} 
		if (Input.GetButton ("Joy1ButtB") && Input.GetButton ("Joy2ButtB")) {
			//exit game
			print ("Exit");
		}*/
	}
}

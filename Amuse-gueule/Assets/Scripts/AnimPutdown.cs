using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPutdown : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagerScript.Instance.getStartGame ()) {
			//game start
			anim.Play ("Putdown");
		} 
	}
}

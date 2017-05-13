using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFold : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.P)) {
			//game start
			anim.Play ("Fold");
		} 
	}
}

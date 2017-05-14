using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPutdown : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
	}

	public void PutDown () {
		anim.Play ("Putdown");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (GameManagerScript.Instance.getRaiseHeadTrigger ()) 
		{
			anim.Play ("RaiseHead");
		}
	}
}

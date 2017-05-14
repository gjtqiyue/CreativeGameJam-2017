using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var cam = GameObject.Find ("Main Camera Environment");
		gameObject.transform.LookAt (cam.transform);
	}
}

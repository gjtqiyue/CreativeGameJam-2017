using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonText : MonoBehaviour {

	public GameObject beatGameText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh t = (TextMesh)beatGameText.GetComponent(typeof(TextMesh));
		if (GameManagerScript.Instance.TimeIsUp ())
			t.text = "Ooops the time is up";
		if (GameManagerScript.Instance.NoBugsLeft ())
			t.text = "Congradulations";

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
	}

	// Update is called once per frame
	public void RaiseCamera () {
		anim.Play ("RaiseHead");
	}

    public void ActionAfterCameraRaised()
    {
        Debug.Log("Camera Raised");
        GameManagerScript.Instance.GetName();
    }
}

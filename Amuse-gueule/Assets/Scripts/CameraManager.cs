using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Animator anim;

	public void RaiseCamera () {
		anim = GetComponent<Animator> ();
		anim.Play ("RaiseHead");
	}

    public void ActionAfterCameraRaised()
    {
        Debug.Log("Camera Raised");
        GameManagerScript.Instance.GetName();
    }

	public void DuckCamera () {
		anim = GetComponent<Animator> ();
		anim.Play ("Duckhead");
	}

	public void ActionAfterCameraDucked()
	{
		Debug.Log("Camera Ducked");
		GameManagerScript.Instance.StartGame ();

	}
}

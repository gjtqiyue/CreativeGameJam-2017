using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPutdown : MonoBehaviour {

	public Animator anim;
    public AudioSource menuPutDownAudio;
    public AudioSource grognementAudio;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
	}

	public void PutUp () {
		anim.Play ("Putup");
	}

	public void PutDown () {
		anim.Play ("Putdown");
	}

    public void playSFXPutDown()
    {
        menuPutDownAudio.Play();
    }

    public void playGrognement()
    {
        grognementAudio.Play();
    }
}

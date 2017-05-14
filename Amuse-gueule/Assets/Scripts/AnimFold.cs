using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFold : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent <Animator> ();
	}

	public void Fold () {
		anim.Play ("Fold");
	}

    public void Unfold()
    {
        anim.Play("Unfold");
    }
}

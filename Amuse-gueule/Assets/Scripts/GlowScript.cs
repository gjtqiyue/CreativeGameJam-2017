using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {

    public Material glowMaterial;
    public Material originalMaterial;
    public Material emergencyMaterial;

    private Material MaterialToReach;

    public float originalTimeToSwitch; // original time to switch between materials
    public float currentTimeToSwitch; // current time to switch between materials
    public float emergencyTimeToSwitch; // emergency time to switch between materials
    public float timer;

    private bool glow;


    // Use this for initialization
    void Start () {
        glow = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagerScript.Instance.scoreManager.comboActivated)
        {
            if (GameManagerScript.Instance.scoreManager.emergencyState)
            {
                MaterialToReach = emergencyMaterial;
                currentTimeToSwitch = emergencyTimeToSwitch;
                // target, amount, time
                iTween.ShakePosition(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.1f);
            } else
            {
                MaterialToReach = glowMaterial;
                currentTimeToSwitch = originalTimeToSwitch;
            }

            if (glow)
            {
                GetComponent<MeshRenderer>().material.Lerp(originalMaterial, MaterialToReach, timer/currentTimeToSwitch);
            } else if (!glow)
            {
                GetComponent<MeshRenderer>().material.Lerp(MaterialToReach, originalMaterial, timer/currentTimeToSwitch);
            }

            if (timer > currentTimeToSwitch)
            {
                // switch between materials
                timer = 0.0f;
                glow = !glow;
                // currentTimeToSwitch = originalTimeToSwitch / 1.25f;
            }

            timer += Time.deltaTime;
        } else
        {
            glow = true;
            GetComponent<MeshRenderer>().material = originalMaterial;
            currentTimeToSwitch = originalTimeToSwitch;
            timer = 0.0f;
            currentTimeToSwitch = originalTimeToSwitch;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {

    public Material glowMaterial;
    public Material originalMaterial;
    public Material emergencyMaterial;

    public Material currentStartMaterial;
    public Material currentEndMaterial;
    public Material MaterialToReach;

    public float originalTimeToSwitch; // original time to switch between materials
    public float currentTimeToSwitch; // current time to switch between materials
    public float timer;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagerScript.Instance.scoreManager.comboActivated)
        {            
            if (GameManagerScript.Instance.scoreManager.emergencyState)
            {
                MaterialToReach = emergencyMaterial;
            } else
            {
                MaterialToReach = glowMaterial;
            }
            if (currentStartMaterial == originalMaterial)
            {
                GetComponent<MeshRenderer>().material.Lerp(originalMaterial, MaterialToReach, timer/currentTimeToSwitch);
            } else if (currentStartMaterial == MaterialToReach)
            {
                GetComponent<MeshRenderer>().material.Lerp(MaterialToReach, originalMaterial, timer/currentTimeToSwitch);
            }
            if (timer > currentTimeToSwitch)
            {
                // switch between materials
                timer = 0.0f;
                if (currentStartMaterial == MaterialToReach)
                {
                    currentStartMaterial = originalMaterial;
                    currentEndMaterial = MaterialToReach;
                }
                else if (currentStartMaterial == originalMaterial)
                {
                    currentStartMaterial = MaterialToReach;
                    currentEndMaterial = originalMaterial;
                }
                currentTimeToSwitch = originalTimeToSwitch / 1.25f;
            }
            timer += Time.deltaTime;
        } else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
            currentTimeToSwitch = originalTimeToSwitch;
            timer = 0.0f;
            currentStartMaterial = originalMaterial;
            currentEndMaterial = glowMaterial;
            currentTimeToSwitch = originalTimeToSwitch;
        }
	}
}

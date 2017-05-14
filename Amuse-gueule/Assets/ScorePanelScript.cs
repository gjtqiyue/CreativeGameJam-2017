using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelScript : MonoBehaviour {

    public AudioSource sfxFacture;

    public void playSFXFacture()
    {
        sfxFacture.Play();
    }
}

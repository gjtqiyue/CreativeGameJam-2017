using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncerAudio : MonoBehaviour {

    public GameObject announcer;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayAnnouncerRandom()
    {
        AudioSource[] audios = announcer.GetComponents<AudioSource>();
        int random = Random.Range(0, 8);
        audios[random].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] themes;

    AudioSource audioS;
    int aux, rnd;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
        
        Shuffle();
        
    }

    void Shuffle()
    {
        if (!audioS.isPlaying)
        {
            rnd = Random.Range(0, 4);
            if (rnd != aux)
            {
                print("Rnd " + rnd);
                audioS.clip = themes[rnd];
                audioS.Play();
            }
            aux = rnd;
        }
    }
}

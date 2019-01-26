using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour {

    public FadeInController fadeinScript;
    public AudioSource audioS;
    public float volume;

    bool lowVolume;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lowVolume = true;
            fadeinScript.gameObject.SetActive(true);
            fadeinScript.LoadScene = "TownMap";
            fadeinScript.enabled = true;
            fadeinScript.End = true;
        }
        if (lowVolume)
            FadeVolume();
    }

    void FadeVolume()
    {
        audioS.volume -= volume;
    }
}

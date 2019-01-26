using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGameManager : MonoBehaviour {

    public GameObject fade;
    public AudioSource rainSound, music, thunderSound;
    public float volume;

    FadeInController fadeInScript;
    bool lowVolume;

    private void Awake()
    {
        fadeInScript = fade.GetComponent<FadeInController>();
    }
    private void Update()
    {
        if (lowVolume)
        {
            FadeVolume();
        }
    }
    public void NewGame()
    {
        lowVolume = true;
        fadeInScript.LoadScene = "Intro_Forest";
        fadeInScript.End = true;
        fadeInScript.enabled = true;
        
    }

    void FadeVolume()
    {
        rainSound.volume -= volume;
        music.volume -= volume;
        thunderSound.volume -= volume;
    }
}

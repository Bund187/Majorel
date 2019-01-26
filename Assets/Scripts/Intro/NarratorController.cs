using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NarratorController : MonoBehaviour {

    
    //public string[] narrator;
    public float waitTime;
    public GameObject fade,livingWoods,majorel,backgrnd,houseIn, sun, sun2, majorelCliff,city;
    public LoadXml_Narrator loadXmlScript;

    TextAppearanceManager textAppearanceManager;
    Text text;
    int i;
    bool hold, canContinue;
    float time;
    NarratorTextClass narratorSentences;
    Animator anim;
    FadeOutController fadeoutScript;
    FadeInController fadeInScript;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        i = 0;
        textAppearanceManager = GetComponent<TextAppearanceManager>();
        canContinue = true;
        narratorSentences = loadXmlScript.NarratorClass;
        anim = majorelCliff.GetComponent<Animator>();
        fadeoutScript = fade.GetComponent<FadeOutController>();
        fadeInScript = fade.GetComponent<FadeInController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!hold)
        {
            if (i < narratorSentences.sentence.Count && canContinue)
                ShowLine();
        }
        else
            Wait();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MagicSpeaker"))
        {
            print("nombre de animacion correcto");
        }
	}

    void ShowLine()
    {
        textAppearanceManager.Sentence = narratorSentences.sentence[i].ToCharArray();
        textAppearanceManager.Text = text;
        textAppearanceManager.enabled = true;
        if (textAppearanceManager.Index == narratorSentences.sentence[i].Length)
        {
            hold = true;
            time = Time.time+2;
            i++;
        }
        if (i >= narratorSentences.sentence.Count)
        {
            Wait();
        }
    }

    void Wait()
    {
        if (Time.time > time + waitTime)
        {
            hold = false;
            textAppearanceManager.enabled = false;
            textAppearanceManager.ResetTextAppeareance();
            switch (i)
            {
                case 3:
                    canContinue = false;
                    break;

                case 5:
                    fade.SetActive(true);
                    break;

                case 8:
                    fadeInScript.enabled = false;
                    livingWoods.SetActive(true);
                    sun.SetActive(false);
                    sun2.SetActive(true);
                    backgrnd.SetActive(false);
                    houseIn.SetActive(false);
                    fadeoutScript.enabled = true;
                    majorel.SetActive(false);
                    break;
                case 11:
                    fadeInScript.enabled = true;
                    break;
                case 12:
                    fadeoutScript.enabled = true;
                    livingWoods.SetActive(false);
                    majorelCliff.SetActive(true);
                    break;
                case 15:
                    fadeInScript.enabled = true;
                    break;
                case 16:
                    fadeoutScript.enabled = true;
                    city.SetActive(false);
                    anim.SetBool("alley", true);
                    break;
                case 18:
                    anim.SetBool("magic", true);
                    break;

            }

        }
    }

    public bool CanContinue
    {
        get
        {
            return canContinue;
        }

        set
        {
            canContinue = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Narrator : AutomaticText {

    public GameObject fade, livingWoods, majorel, backgrnd, houseIn, sun, sun2, sun3, majorelCliff, city, houseTorch, alleyTorch1, alleyTorch2, magicEncounterSpeak;
    public LoadXml_Narrator loadXmlScript;
    public int timeTravel;
    
    Animator anim;
    NarratorTextClass narratorSentences;
    bool canContinue;
    FadeOutController fadeoutScript;
    FadeInController fadeInScript;

    void Start () {
        anim = majorelCliff.GetComponent<Animator>();
        narratorSentences = loadXmlScript.NarratorClass;
        fadeInScript = fade.GetComponent<FadeInController>();
        fadeoutScript = fade.GetComponent<FadeOutController>();
        i = 0;
        canContinue = true;
        text = GetComponent<Text>();
        textAppearanceManager = GetComponent<TextAppearanceManager>();
    }

    void Update()
    {
        Time.timeScale = timeTravel;
        if (!hold)
        {
            if (i < narratorSentences.sentence.Count && canContinue)
            {
               ShowLine(narratorSentences.sentence);
            }
        }
        else
            Wait();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MagicSpeaker"))
        {
            magicEncounterSpeak.SetActive(true);
        }
    }

    protected override void LineException()
    {
        switch (i)
        {
            case 3:
                canContinue = false;
                fade.SetActive(false);
                fadeInScript.enabled = true;
                fadeoutScript.enabled = false;
                Color alpha = fade.GetComponent<Image>().color;
                alpha.a = 0;
                fade.GetComponent<Image>().color=alpha;
                break;

            case 6:
                fade.SetActive(true);
                houseTorch.SetActive(false);
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
                houseTorch.SetActive(true);
                fadeoutScript.enabled = true;
                livingWoods.SetActive(false);
                majorelCliff.SetActive(true);
                break;
            case 15:
                fadeInScript.enabled = true;
                break;
            case 16:
                houseTorch.SetActive(false);
                fadeoutScript.enabled = true;
                city.SetActive(false);
                anim.SetBool("alley", true);
                break;
            case 19:
                fadeInScript.enabled = true;
                break;
            case 20:
                fadeoutScript.enabled = true;
                alleyTorch1.SetActive(true);
                alleyTorch2.SetActive(true);
                sun2.SetActive(false);
                sun3.SetActive(true);
                anim.SetBool("magic", true);
                break;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MagicEncounter : AutomaticText {

    public LoadXml_Narrator loadXmlScript;
    public GameObject majorelText, background;
    public FadeInController fadeinScript;

    NarratorTextClass narratorSentences;
    Animator anim;
    TextAppearanceManager majorelTextAppearanceManager;
    void Start() {
        majorelText.SetActive(true);
        narratorSentences = loadXmlScript.NarratorClass;
        text = GetComponent<Text>();
        textAppearanceManager = GetComponent<TextAppearanceManager>();
        i = 0;
        anim = background.GetComponent<Animator>();
        majorelTextAppearanceManager = majorelText.GetComponent<TextAppearanceManager>();
    }

    void Update()
    {
        if (!hold)
        {
            if (i < narratorSentences.entitySentence.Count)
            {
                ShowLine(narratorSentences.entitySentence);
            }
        }
        else
            Wait();
    }

    protected override void LineException()
    {
        switch (i)
        {
            case /*1*/1:
                MajorelShow(i,true);
                break;
            case /*2*/2:
                MajorelShow(i, false);
                break;
            case /*6*/4:
                MajorelShow(i, true);
                break;
            case /*7*/5:
                MajorelShow(i, false);
                break;
            case /*9*/6:
                anim.SetTrigger("getpowers");
                MajorelShow(i, true);
                break;
            case /*10*/7:
                MajorelShow(i, false);
                break;
            case /*12*/9:
                anim.SetBool("gone",true);
                fadeinScript.LoadScene = "TownMap";
                fadeinScript.enabled = true;
                fadeinScript.End = true;
                break;
        }
    }

    void MajorelShow(int j,bool active)
    {
        print("entra " + j);
        majorelTextAppearanceManager.Sentence = narratorSentences.majorelSentence[j].ToCharArray();

        if(!active)
            majorelTextAppearanceManager.ResetTextAppeareance();
        else
            majorelTextAppearanceManager.Text = majorelText.GetComponent<Text>();

        majorelTextAppearanceManager.enabled = active;
        
    }
}

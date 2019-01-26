using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroText : MonoBehaviour {

    public TextAppearanceManager textScript;
    public LoadXml_Misc xmlScript;
    public PlayerController playerScript;

    private void Update()
    {
        print("go intro" + gameObject.name);
        playerScript.IsTalking = true;
        textScript.speed = 0.05f;
        textScript.Text = GetComponent<Text>();
        textScript.Sentence = xmlScript.MiscClass.introText.ToCharArray();
        textScript.enabled = true;
        if(textScript.Index>= xmlScript.MiscClass.introText.Length)
        {
            textScript.speed = 0.02f;
            playerScript.IsTalking = false;
            this.enabled = false;
        }
    }
}

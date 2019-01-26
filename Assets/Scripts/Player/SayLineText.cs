using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SayLineText : MonoBehaviour {

    private TextAppearanceManager textAppearanceManager;
    private Text text;

    private void Awake()
    {
        textAppearanceManager = GetComponent<TextAppearanceManager>();
        text = GetComponent<Text>();
    }

    public void Talk(string sentence)
    {
        if (textAppearanceManager != null)
        {
            textAppearanceManager.Text = text;
            textAppearanceManager.Sentence = sentence.ToCharArray();
            textAppearanceManager.enabled = true;
        }

    }
}

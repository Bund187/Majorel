using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseStair : ActionManager
{

    public TextAppearanceManager textAppearScript;
    public Text playerText;
    public LoadXml_Misc loadXml;

    public override void PerformAction()
    {
        textAppearScript.Text = playerText;
        textAppearScript.Sentence = loadXml.MiscClass.stairClosed.ToCharArray();
        textAppearScript.enabled = true;
    }
}

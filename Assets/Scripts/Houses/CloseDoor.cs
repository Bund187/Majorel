using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDoor : ActionManager {

    public TextAppearanceManager textAppearScript;
    public Text playerText;
    public LoadXml_Misc loadXml;
    public bool isBridge;

    public override void PerformAction()
    {
        textAppearScript.Text = playerText;
        if(!isBridge)
            textAppearScript.Sentence = loadXml.MiscClass.doorClosed.ToCharArray();
        else
            textAppearScript.Sentence = loadXml.MiscClass.bridgeClosure.ToCharArray();

        textAppearScript.enabled = true;
    }
}

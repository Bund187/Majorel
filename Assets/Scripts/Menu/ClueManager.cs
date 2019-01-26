using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClueManager : MonoBehaviour {

    public LoadXml_Misc loadXmlScript;
    public Text[] smallNotes;
    public Text[] bigNotes;

    List<string> notes = new List<string>();

    public void FillNotesWithClue(string key)
    {
        //if (español) { }  AQUI ME HE DE ENCARGAR DEL IDIOMA

        if (key == "wino")
        {
            notes.Add(loadXmlScript.MiscClass.wino);
        }
        if (key == "snake")
        {
            notes.Add(loadXmlScript.MiscClass.snake);
        }
        if (key == "trap"){
            notes.Add(loadXmlScript.MiscClass.trap);
        }
        if (key == "burn")
        {
            notes.Add(loadXmlScript.MiscClass.burn);
        }
        if (key == "straw")
        {
            notes.Add(loadXmlScript.MiscClass.straw);
        }
        if (key == "thug")
        {
            notes.Add(loadXmlScript.MiscClass.thug);
        }
        if (key == "lover")
        {
            notes.Add(loadXmlScript.MiscClass.lover);
        }


        for (int i = 0; i < notes.Count; i++)
        {
            if (i < smallNotes.Length)
            {
                smallNotes[i].text = notes[i];
            }
            bigNotes[i].text = notes[i];
        }
    }
}

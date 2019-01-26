using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DoorDialogue : ActionManager {

    public Text text;
    public TextAppearanceManager textAppearanceManager;
    public LoadXml_Misc miscClass;
    public GameObject entrance;
    public DictionaryEvent dictionaryE;

    bool haveAlcohol;

    public override void PerformAction()
    {
        if (!HaveAlcohol)
        {
            dictionaryE.Events["drunk"] = true;
            textAppearanceManager.Sentence = miscClass.MiscClass.mathiasDoorStop.ToCharArray();
        }
        else
        {
            dictionaryE.Events["drunk"] = false;
            textAppearanceManager.Sentence = miscClass.MiscClass.mathiasDoorGo.ToCharArray();
            entrance.SetActive(true);
            this.gameObject.SetActive(false);
        }
        textAppearanceManager.Text = text;
        textAppearanceManager.enabled = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            textAppearanceManager.enabled = false;
            textAppearanceManager.ResetTextAppeareance();
            text.text = "";
        }
    }
    public bool HaveAlcohol
    {
        get
        {
            return haveAlcohol;
        }

        set
        {
            haveAlcohol = value;
        }
    }
}

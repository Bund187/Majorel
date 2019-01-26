using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatueDialogue : ActionManager
{

    public Text text;
    public TextAppearanceManager textAppearanceManager;
    public LoadXml_Misc miscClass;

    public override void PerformAction()
    {
        if(gameObject.name.Contains("Golem"))
            textAppearanceManager.Sentence = miscClass.MiscClass.golemStatue.ToCharArray();
        if (gameObject.name.Contains("Death"))
            textAppearanceManager.Sentence = miscClass.MiscClass.golemStatue.ToCharArray();
        if (gameObject.name.Contains("Joker"))
            textAppearanceManager.Sentence = miscClass.MiscClass.golemStatue.ToCharArray();
        if (gameObject.name.Contains("Creature"))
            textAppearanceManager.Sentence = miscClass.MiscClass.golemStatue.ToCharArray();
        if (gameObject.name.Contains("Beggar"))
            textAppearanceManager.Sentence = miscClass.MiscClass.golemStatue.ToCharArray();
        textAppearanceManager.Sentence = miscClass.MiscClass.deathStatue.ToCharArray();
        textAppearanceManager.Sentence = miscClass.MiscClass.beggarStatue.ToCharArray();
        textAppearanceManager.Sentence = miscClass.MiscClass.jokerStatue.ToCharArray();
        textAppearanceManager.Sentence = miscClass.MiscClass.creatureStatue.ToCharArray();
        
        this.gameObject.SetActive(false);
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
}

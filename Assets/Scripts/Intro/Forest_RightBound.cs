using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Forest_RightBound : MonoBehaviour {

    public Text text;
    public TextAppearanceManager textAppearanceManager;
    public string sentence;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textAppearanceManager.Text = text;
            textAppearanceManager.Sentence = sentence.ToCharArray();
            textAppearanceManager.ResetTextAppeareance();
            textAppearanceManager.enabled = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.text = "";
            textAppearanceManager.ResetTextAppeareance();
            textAppearanceManager.enabled = false;
        }
    }
}

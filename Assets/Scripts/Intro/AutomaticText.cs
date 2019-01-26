using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AutomaticText : MonoBehaviour
{

    public float waitTime;
    public float timeSpeed;

    protected bool hold;
    protected int i;
    protected TextAppearanceManager textAppearanceManager;
    protected Text text;
    float time;
    
    protected void ShowLine(List<string> arrayText)
    {
        textAppearanceManager.Sentence = arrayText[i].ToCharArray();
        textAppearanceManager.Text = text;
        textAppearanceManager.enabled = true;
        if (textAppearanceManager.Index == arrayText[i].Length)
        {
            hold = true;
            time = Time.time + timeSpeed;
            i++;
        }
        if (i >= arrayText.Count)
        {
            Wait();
        }
    }

    protected void Wait()
    {
        if (Time.time > time + waitTime)
        {
            hold = false;
            textAppearanceManager.enabled = false;
            textAppearanceManager.ResetTextAppeareance();
            LineException();
        }
    }

    protected virtual void LineException()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDispatcher : MonoBehaviour {

    protected Object[] dialogues;
    protected string[] majorelDialogue;
    protected string[] mathiasDialogue;

    void Awake()
    {
        dialogues = Resources.LoadAll("3", typeof(TextAsset));
        //mathiasDialogue = Resources.LoadAll("Mathias_Dialogue", typeof(TextAsset));
        //    majorelDialogue = dialogues[0].ToString().Split('_');
        

    }
    protected Object[] LoadDialogue(int index)
    {
        Object[] dialogue;
        dialogue = Resources.LoadAll("0", typeof(TextAsset));
        //print("Dialogo " + dialogue[0].ToString());
        return dialogue;
    }
    public string[] Majorel
    {
        get
        {
            return majorelDialogue;
        }

        set
        {
            majorelDialogue = value;
        }
    }

    public string[] RandomText
    {
        get
        {
            return mathiasDialogue;
        }

        set
        {
            mathiasDialogue = value;
        }
    }

    public Object[] Dialogues
    {
        get
        {
            return dialogues;
        }

        set
        {
            dialogues = value;
        }
    }
}

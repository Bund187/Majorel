using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
public class DialogueNpcClass{

    public string name;
    public List<string> dialogueLines = new List<string>();
}

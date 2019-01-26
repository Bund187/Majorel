using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
public class DialogueTypeClass{

    public string name;
    public List<DialogueNpcClass> npc = new List<DialogueNpcClass>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
[XmlRoot("DialogueRoot")]

public class NarratorTextClass{

    public List<string> sentence=new List<string>();
    public List<string> majorelSentence = new List<string>();
    public List<string> entitySentence = new List<string>();
}

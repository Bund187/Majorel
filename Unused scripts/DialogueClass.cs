using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
[XmlRoot("DialogueRoot")]
public class DialogueClass{

    public List<CharacterDialogue> characterSpeaker = new List<CharacterDialogue>();
}

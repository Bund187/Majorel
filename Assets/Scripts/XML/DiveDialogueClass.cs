using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("Dialogue_Root")]
public class DiveDialogueClass{

    public List<DialogueTypeClass> dialogueTypes = new List<DialogueTypeClass>();
}

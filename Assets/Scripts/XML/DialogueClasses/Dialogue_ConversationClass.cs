using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
[XmlRoot("DialogueRoot")]
public class Dialogue_ConversationClass{

    public string returnLine;
    public string shop;
    public string playMeoan;
    public string wantToAsk;
    public string needInformation;
    public Dialogue_FarewellClass farewellClass;
    public Dialogue_GreetingClass greetingClass;
    public Dialogue_RumourClass rumourClass;
    public List<Dialogue_UniqueQuestionClass> uniqueClass = new List<Dialogue_UniqueQuestionClass>();
}

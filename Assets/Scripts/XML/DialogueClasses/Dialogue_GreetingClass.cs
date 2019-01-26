using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
public class Dialogue_GreetingClass{

    public List<string> greetingsQuestions = new List<string>();
    public List<string> greetingsAnswers = new List<string>();
}

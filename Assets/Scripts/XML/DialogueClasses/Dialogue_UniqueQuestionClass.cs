using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
public class Dialogue_UniqueQuestionClass{

    public int speakers;
    public int listener;
    public int exception;
    public int exceptionAtLine;
    public List<string> keys = new List<string>();
    public List<string> uniqueQuestion = new List<string>();
    public List<string> uniqueAnswers = new List<string>();
}

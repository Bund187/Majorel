using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResponseManager : MonoBehaviour {

    public GameObject clueIcon;
    public DialogueEventTrigger eventTrigger;
    public DictionaryEvent dictionaryE;

    static List<string> foundedClues = new List<string>();

    List<string> farewellResponse = new List<string>();
    List<string> rumourResponse = new List<string>();
    List<string> greetingResponse = new List<string>();
    List<string> generalUniqueResponse = new List<string>();
    List<string> specificUniqueResponse = new List<string>();
    List<string> cluesToFind = new List<string>();
    TextAppearanceManager TextAppearanceManager;
    GameObject listener;

    public void ShowResponse(int i, bool showGeneralresponse, bool showUniqueResponse, DialogueNavigation dialogueScript)
    {
        string sentence = null;
        if (!showGeneralresponse && !showUniqueResponse)
        {
            int rnd;
            switch (i)
            {
                case 0:
                    rnd = Random.Range(0, greetingResponse.Count);
                    sentence = greetingResponse[rnd];
                    break;
                case 1:
                    rnd = Random.Range(0, rumourResponse.Count);
                    sentence = rumourResponse[rnd];
                    break;
                default:
                    rnd = Random.Range(0, farewellResponse.Count);
                    sentence = farewellResponse[rnd];
                    break;
            }
        }
        else if (showGeneralresponse)
            sentence = generalUniqueResponse[i];
        else if (showUniqueResponse)
        {
            if (dictionaryE.Events["dated"] && specificUniqueResponse[i].Contains("30")){
                sentence = specificUniqueResponse[i+1];
            }
            else
            {
                sentence = specificUniqueResponse[i];
            }
        }

        if (sentence != null)
        {
            TextAppearanceManager = listener.GetComponent<TextAppearanceManager>();
            TextAppearanceManager.Text = listener.GetComponent<Text>();
            TextAppearanceManager.Sentence = sentence.ToCharArray();

            if(dialogueScript!=null)
                TextAppearanceManager.DialogueScript = dialogueScript;

            TextAppearanceManager.enabled = true;

            ClueSearch(sentence);
        }

        //LLAMAMOS AL SCRIPT QUE ACTIVA UN EVENTO SEGÚN LO QUE HAYA RESPONDIDO EL NPC
        eventTrigger.SentenceEventTrigger(sentence, listener.transform.parent.parent.name);
        
    }
    public void ClueSearch(string sentence)
    {
        bool clueFounded = false;
        for (int k = 0; k < foundedClues.Count; k++)
        {
            if (foundedClues[k].Equals(sentence))
                clueFounded = true;
        }
        if (!clueFounded)
        {
            for (int j = 0; j < cluesToFind.Count; j++)
            {
                if (cluesToFind[j].Equals(sentence))
                {
                    foundedClues.Add(sentence);
                    clueIcon.GetComponent<ClueIconBehaviour>().Temp = Time.time;
                    clueIcon.SetActive(true);

                    //LLAMAMOS AL SCRIPT QUE RELLENA LAS NOTAS
                    GetComponent<ClueManager>().FillNotesWithClue(sentence);
                    Debug.Log("Se añadiría una pista al log");
                }
            }
        }
    }
    public void HideResponse()
    { 

        TextAppearanceManager = listener.GetComponent<TextAppearanceManager>();
        TextAppearanceManager.Text = listener.GetComponent<Text>();
        TextAppearanceManager.Text.text = "";
    }

    public void FillOtherLine(Dialogue_ConversationClass dialogueClass_Class)
    {

        for (int i=0;i<dialogueClass_Class.rumourClass.rumoursAnswers.Count;i++)
        {
            string rumour = dialogueClass_Class.rumourClass.rumoursAnswers[i];
            rumour = CheckClue(rumour);
            rumourResponse.Add(rumour);
        }
        foreach (string greeting in dialogueClass_Class.greetingClass.greetingsAnswers)
        {
            greetingResponse.Add(greeting);
        }
        foreach (string farewell in dialogueClass_Class.farewellClass.farewell)
        {
            farewellResponse.Add(farewell);
        }
    }

    public void SearchSpecificAnswers(Dialogue_ConversationClass dialogueClass_Class, int i_Listener, int i_Speaker, DictionaryEvent dictionaryE)
    {
        Debug.Log("Cantidad de clues encontradas " + foundedClues.Count);
        for (int i = 0; i < dialogueClass_Class.uniqueClass.Count; i++)
        {
            //BOOLEANO QUE COMPRUEBA SI CIERTO EVENTO HA SIDO DISPARADO O NO
            bool triggerEvent = false;
            //RECORREMOS LOS DIALOGOS DENTRO DEL XML SEGUN EL PERSONAJE QUE HABLE CON OTRO
            for (int j = 0; j < dialogueClass_Class.uniqueClass[i].uniqueQuestion.Count; j++)
            {
                //COMPROBAMOS QUE PERSONAJE HABLA CON CUAL
                if (dialogueClass_Class.uniqueClass[i].speakers == i_Speaker && (dialogueClass_Class.uniqueClass[i].listener == i_Listener || dialogueClass_Class.uniqueClass[i].listener == -1) || (dialogueClass_Class.uniqueClass[i].speakers == -1 && dialogueClass_Class.uniqueClass[i].listener == -1))
                {
                    //SI HAY KEYS DE LOS EVENTOS EN ESTE DIALOGO
                    if (dialogueClass_Class.uniqueClass[i].keys.Count > 0)
                    {
                        //SI LA KEY DEL XML NO ESTA VACIA
                        if (!dialogueClass_Class.uniqueClass[i].keys[j].Equals(""))
                        {
                            //SI LA KEY DEL EVENTO ENCONTRADA SE ENCUENTRA ENTRE NUESTROS EVENTOS
                            if (dictionaryE.Events.ContainsKey(dialogueClass_Class.uniqueClass[i].keys[j]))
                            {
                                //COMPROBAMOS VALOR TRUE O FALSE SI NUESTRO EVENTO ESTA ACTIVADO O NO
                                if (dictionaryE.Events.TryGetValue(dialogueClass_Class.uniqueClass[i].keys[j], out triggerEvent))
                                {
                                    //LE DAMOS DICHO VALOR A NUESTRO BOOLEANO
                                    dictionaryE.Events[dialogueClass_Class.uniqueClass[i].keys[j]] = triggerEvent;
                                }
                            }
                        }
                        else
                            triggerEvent = true;
                    }
                    else
                    {
                        triggerEvent = true;
                    }
                }
                if (dialogueClass_Class.uniqueClass[i].exception != -1)
                {
                    if (dialogueClass_Class.uniqueClass[i].exception != i_Listener)
                    {
                        AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, dialogueClass_Class.uniqueClass[i].uniqueAnswers[j], triggerEvent);
                    }
                    else
                    {
                        if (dialogueClass_Class.uniqueClass[i].exceptionAtLine != j)
                        {
                            AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, dialogueClass_Class.uniqueClass[i].uniqueAnswers[j], triggerEvent);
                        }
                    }
                }
                else
                {
                    AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, dialogueClass_Class.uniqueClass[i].uniqueAnswers[j], triggerEvent);
                }
            }
        }
    }

    string CheckClue(string toCheck)
    {
        if (toCheck.StartsWith("@_"))
        {
            toCheck = toCheck.Substring(2);
            cluesToFind.Add(toCheck);
        }
        return toCheck;
    }
    public void AddQuestion(int xmlSpeaker, int i_Speaker, int xmlListener, int i_Listener, string answer, bool isEventTriggered)
    {
        answer = CheckClue(answer);
        if (isEventTriggered)
        {
            if (xmlSpeaker == i_Speaker && (xmlListener == i_Listener || xmlListener == -1))
            {
                if (xmlSpeaker == 0 && specificUniqueResponse.Count <= 0)
                {
                    specificUniqueResponse.Add(answer+listener.transform.parent.parent.name);
                }
                else
                {
                    specificUniqueResponse.Add(answer);
                }
                
            }
            if (xmlSpeaker == -1 && xmlListener == -1)
            {
                //Debug.Log("Añadimos respuesta general: " + answer);
                generalUniqueResponse.Add(answer);
            }
        }
    }

    public void ResetAnswers()
    {
        rumourResponse.Clear();
        greetingResponse.Clear();
        generalUniqueResponse.Clear();
        specificUniqueResponse.Clear();
        cluesToFind.Clear();
    }

    public List<string> GeneralUniqueResponses
    {
        get
        {
            return generalUniqueResponse;
        }

        set
        {
            generalUniqueResponse = value;
        }
    }

    public List<string> RumourResponse
    {
        get
        {
            return rumourResponse;
        }

        set
        {
            rumourResponse = value;
        }
    }

    public List<string> GreetingResponse
    {
        get
        {
            return greetingResponse;
        }

        set
        {
            greetingResponse = value;
        }
    }

    public List<string> SpecificUniqueResponse
    {
        get
        {
            return specificUniqueResponse;
        }

        set
        {
            specificUniqueResponse = value;
        }
    }

    public List<string> FarewellResponse
    {
        get
        {
            return farewellResponse;
        }

        set
        {
            farewellResponse = value;
        }
    }

    public GameObject Listener
    {
        get
        {
            return listener;
        }

        set
        {
            listener = value;
        }
    }
}

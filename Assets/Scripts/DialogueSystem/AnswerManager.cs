using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager{

    List<string> generalUniqueQuestions = new List<string>();
    List<string> specificUniqueQuestions = new List<string>();
    List<string> firstLevelDialogue = new List<string>();
    
    public void FillUniqueQuestions(Dialogue_ConversationClass dialogueClass_Class, int i_Listener, int i_Speaker, DictionaryEvent dictionaryE)
    {
        //RECORREMOS TODA LA CLASE QUE HA LEIDO EL XML
        for (int i=0;i<dialogueClass_Class.uniqueClass.Count;i++)
        {
            //BOOLEANO QUE COMPRUEBA SI CIERTO EVENTO HA SIDO DISPARADO O NO
            bool triggerEvent=false;
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
                                    Debug.Log("Key dialogo " + dialogueClass_Class.uniqueClass[i].keys[j]);
                                    //LE DAMOS DICHO VALOR A NUESTRO BOOLEANO
                                    dictionaryE.Events[dialogueClass_Class.uniqueClass[i].keys[j]] = triggerEvent;
                                    //Debug.Log("Se ha encontrado la key " + dialogueClass_Class.uniqueClass[i].keys[j] + " de indice " + j + " la cual es: " + triggerEvent);
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
                
                //SI EXISTE UN PERSONAJE EXCEPCION EN NUESTRO DIALOGO
                if (dialogueClass_Class.uniqueClass[i].exception != -1)
                {
                    //SI DICHA EXCEPCION NO ES EL PERSONAJE CON QUE ESTAMOS HABLANDO
                    if (dialogueClass_Class.uniqueClass[i].exception != i_Listener)
                    {
                        //AÑADIMOS LA PREGUNTA
                        AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, dialogueClass_Class.uniqueClass[i].uniqueQuestion[j],triggerEvent);
                    }
                    else //SI EL PERSONAJE CON QUE HABLAMOS ES LA EXCEPCIÓN
                    {
                        //AÑADIMOS TODAS AQUELLAS PREGUNTAS ESCEPTO LA DE INDICE EXCEPTIONATLINE
                        if (dialogueClass_Class.uniqueClass[i].exceptionAtLine != j)
                        {
                            AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, dialogueClass_Class.uniqueClass[i].uniqueQuestion[j],triggerEvent);
                        }
                    }
                }
                else //SI NO EXISTE UN PERSONAJE EXCEPCION EN NUESTRO DIALOGO
                {
                    if (!dictionaryE.Events["dated"])
                    {

                        if (!dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Contains("/_"))
                        {
                            string sentence = dialogueClass_Class.uniqueClass[i].uniqueQuestion[j];
                            if (dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Contains("*_"))
                                sentence = dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Substring(2);
                            AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, sentence, triggerEvent);
                        }
                    }
                    else
                    {
                        if (!dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Contains("*_"))
                        {
                            string sentence = dialogueClass_Class.uniqueClass[i].uniqueQuestion[j];
                            if (dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Contains("/_"))
                                sentence = dialogueClass_Class.uniqueClass[i].uniqueQuestion[j].Substring(2);
                            AddQuestion(dialogueClass_Class.uniqueClass[i].speakers, i_Speaker, dialogueClass_Class.uniqueClass[i].listener, i_Listener, sentence, triggerEvent);
                        }
                    }
                }
            }
           

        }
        //AÑADIMOS "ADIOS" EN NUESTROS ARRAYS
        specificUniqueQuestions.Add(dialogueClass_Class.returnLine);
        generalUniqueQuestions.Add(dialogueClass_Class.returnLine);

    }
    void AddQuestion(int xmlSpeaker, int i_Speaker, int xmlListener, int i_Listener, string question,bool isEventTriggered)
    {
        if (isEventTriggered)
        {
            if (xmlSpeaker == i_Speaker && (xmlListener == i_Listener || xmlListener == -1))
            {
                //Debug.Log("Añadimos pregunta unica: " + question);
                specificUniqueQuestions.Add(question);
            }
            if (xmlSpeaker == -1 && xmlListener == -1)
            {
                //Debug.Log("Añadimos pregunta general: " + question);
                generalUniqueQuestions.Add(question);
            }
        }
    }

    public void ResetQuestions()
    {
        generalUniqueQuestions.Clear();
        specificUniqueQuestions.Clear();
        firstLevelDialogue.Clear();
    }

    public List<string> SpecificUniqueQuestions
    {
        get
        {
            return specificUniqueQuestions;
        }

        set
        {
            specificUniqueQuestions = value;
        }
    }

    public List<string> GeneralUniqueQuestions
    {
        get
        {
            return generalUniqueQuestions;
        }

        set
        {
            generalUniqueQuestions = value;
        }
    }

    public List<string> FirstLevelDialogue
    {
        get
        {
            return firstLevelDialogue;
        }

        set
        {
            firstLevelDialogue = value;
        }
    }
}

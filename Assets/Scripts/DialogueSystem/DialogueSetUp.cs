using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSetUp : MonoBehaviour
{

    public Text[] dialogueLines;
    public GameObject dialogueClass, dialogueQuestionsBox, goDictionary;
    public DialogueNavigation DialogueNavigation;
    
    GameObject listener, speaker;
    Dialogue_ConversationClass dialogueClass_Class;
    ResponseManager Responsemanager;
    AnswerManager AnswerManager;
    DictionaryEvent DictionaryE;
    CameraCenterController CameraCenterController;
    
    public void Awake()
    {
        //AÑADIMOS LOS DIALOGOS CARGADOS DEL XML
        dialogueClass_Class = dialogueClass.GetComponent<LoadXml>().DClass;
        AnswerManager = new AnswerManager();
        DictionaryE = goDictionary.GetComponent<DictionaryEvent>();
        CameraCenterController = Camera.main.GetComponent<CameraCenterController>();
    }

    public void SetUpTextBox()
    {
        //HACEMOS ZOOM IN EN LOS PERSONAJES
        CameraCenterController.Talk = true;
        CameraCenterController.ActivateZoom = true;

        //POSICIONAMOS LAS LINEAS DE DIALOGOS DE LOS INTERLOCUTORES
        DialoguelinePosition();

        //ACTIVAMOS LAS TEXTBOX DEL PLAYER
        dialogueQuestionsBox.SetActive(true);

        //IMPEDIMOS QUE EL PLAYER SE MUEVA
        speaker.GetComponent<PlayerController>().IsTalking = true;

        //LE PASAMOS EL SCRIPT DE RESPUESTAS A LA NAVEGACIÓN PARA SU POSTERIOR USO. TAMBIÉN LE PASAMOS EL CANVAS DEL LISTENER QUE CONTIENE LOS SCRIPTS PARA LA RESPUESTA
        Responsemanager = GetComponent<ResponseManager>();
        Responsemanager.Listener= listener.transform.GetChild(1).transform.GetChild(0).gameObject;
        
        //INICIALIZAMOS EL EL SCRIPT QUE CONTIENE LAS PREGUNTAS Y LAS RESPUESTAS
        DialogueNavigation.ResponseManager = Responsemanager;
        DialogueNavigation.AnswerManager = AnswerManager;

        //AÑADIMOS LA CLASE DEL XML A LA NAVEGACION
        DialogueNavigation.DialogueClass_Class = dialogueClass_Class;

        //RECOGEMOS EL INDICE DEL NPC Y DEL PLAYER
        int listenerIndex = listener.GetComponent<PlayerStats>().Stats1.Index;
        int speakerIndex = speaker.GetComponent<PlayerStats>().Stats1.Index;

        //RELLENAMOS LAS PREGUNTAS UNICAS, GENERALES Y ESPECIFICAS
        AnswerManager.FillUniqueQuestions(dialogueClass_Class, listenerIndex, speakerIndex, DictionaryE);

        //RELLENAMOS LAS RESPUESTAS DE RUMORES
        Responsemanager.FillOtherLine(dialogueClass_Class);

        //RELLENAMOS LAS RESPUESTAS ESPECIFICAS
        Responsemanager.SearchSpecificAnswers(dialogueClass_Class, listenerIndex, speakerIndex, DictionaryE);

        //RELLENAMOS LAS LINEAS DE DIALOGO CON EL PRIMER NIVEL DE DIALOGO, TAMBIÉN RELLENAMOS EL LIST DEL PRIMER NIVEL PARA LAS PREGUNTAS
        FillDialogueLines(listenerIndex, speakerIndex);
        
    }

    public void DialoguelinePosition()
    {
        RectTransform playerRect = speaker.transform.GetChild(6).transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform npcRect = listener.transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>();

        float left = -46;
        float right = -43;
        if (speaker.transform.position.x > listener.transform.position.x)
        {
            playerRect.localPosition = new Vector2(right, playerRect.localPosition.y);
            npcRect.localPosition = new Vector2(left, npcRect.localPosition.y);
        }
        else
        {
            playerRect.localPosition = new Vector2(left, playerRect.localPosition.y);
            npcRect.localPosition = new Vector2(right, npcRect.localPosition.y);
        }
    }

    //RELLENA EL PRIMER SET DE PREGUNTAS, EL BÁSICO
    public void FillDialogueLines(int i_Listener, int i_Speaker)
    {
        int i = 0;
        //RANDOMIZAMOS UN SALUDO PARA LA PRIMERA LINEA DE DIALOGO, LINEA 1
        int r = Random.Range(0, dialogueClass_Class.greetingClass.greetingsQuestions.Count);
        dialogueLines[i].text = dialogueClass_Class.greetingClass.greetingsQuestions[r];
        AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.greetingClass.greetingsQuestions[r]);
        i++;
        //RANDOMIZAMOS UNA PREGUNTA DE RUMOR PARA LA SEGUNDA LINEA, LINEA 2
        r = Random.Range(0, dialogueClass_Class.rumourClass.rumoursQuestion.Count);
        dialogueLines[i].text = dialogueClass_Class.rumourClass.rumoursQuestion[r];
        AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.rumourClass.rumoursQuestion[r]);
        i++;
        //SI EL LISTENER NOS PUEDE DAR INFORMACION
        if (AnswerManager.GeneralUniqueQuestions.Count > 1)
        {
            //RECABAR INFORMACION, LINEA 3
            dialogueLines[i].text = dialogueClass_Class.needInformation;
            AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.needInformation);
            i++;
        }
        //SI EXISTEN PREGUNTAS ESPECIFICAS ENTRE LOS INTERLOCUTORES
        if (AnswerManager.SpecificUniqueQuestions.Count > 1)
        {
            //HACER PREGUNTAS, LINEA 3
            dialogueLines[i].text = dialogueClass_Class.wantToAsk;
            AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.wantToAsk);
            i++;
        }
        //LINEA 4, JUGAR AL MEOAN
        dialogueLines[i].text = dialogueClass_Class.playMeoan;
        AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.playMeoan);
        i++;
        //SI EL LISTENER ES EL TENDERO O EL BOTICARIO
        //if (i_Listener == 9 || i_Listener == 14)
        //{
        //    //TIENDA
        //    //dialogueLines[i].text = dialogueClass_Class.shop;
        //    //AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.shop);
        //    //i++;

        //    //ADIOS
        //    print("Adios 1");
        //    r = Random.Range(0, dialogueClass_Class.farewellClass.farewell.Count);
        //    dialogueLines[i].text = dialogueClass_Class.farewellClass.farewell[r];
        //    AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.farewellClass.farewell[r]);
        //    DialogueNavigation.MaxLineNumber = i + 1;
        //}
        //else
        //{
            print("Adios 2");
            //ADIOS, LINEA 5
            r = Random.Range(0, dialogueClass_Class.farewellClass.farewell.Count);
            dialogueLines[i].text = dialogueClass_Class.farewellClass.farewell[r];
            AnswerManager.FirstLevelDialogue.Add(dialogueClass_Class.farewellClass.farewell[r]);
            DialogueNavigation.MaxLineNumber = i+1;

        //}

    }
    
    //SALIMOS DEL DIALOGO, SE LLAMA DESDE LA NAVEGACIÓN
    public void ExitDialogue()
    {
        //DESACTIVAMOS LAS TEXTBOXES DEL DIALOGO
        dialogueQuestionsBox.SetActive(false);

        //VACIAMOS LOS LISTS TANTO DE PREGUNTAS COMO DE RESPUESTAS
        AnswerManager.ResetQuestions();
        Responsemanager.ResetAnswers();

        //HACEMOS ZOOM OUT
        CameraCenterController.Talk = false;
        CameraCenterController.ActivateZoom = false;

        //PERMITIMOS QUE EL PLAYER SE MUEVA
        speaker.GetComponent<PlayerController>().IsTalking = false;
        //PERMITIMOS QUE EL NPC SE MUEVA
        if(listener.GetComponent<PathFind>()!=null)
            listener.GetComponent<PathFind>().enabled = true;
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

    public GameObject Listener1
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

    public GameObject Speaker
    {
        get
        {
            return speaker;
        }

        set
        {
            speaker = value;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueNavigation : MonoBehaviour {

    public GameObject line;
    public DialogueSetUp dialogueSetup;
    public MeoanSetUp meoanSetup;
    public Text[] dialogueLines;

    float time;
    bool isMoving, isAction, activateDialogue, holdKey, isinformation,wantToAsk,isMeoanOn, talking;
    int dialogueLineIndex, clickCounter;
    int maxLineNumber;
    ResponseManager responseManager;
    AnswerManager answerManager;
    Dialogue_ConversationClass dialogueClass_Class;
    bool finishLine;

    // Use this for initialization
    void Start () {
        activateDialogue = true;
        dialogueLineIndex = 0;
        clickCounter = -1;
        print("dialogue go " + gameObject.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isMeoanOn)
        {
            Navigation();
            Wait();
            LineFinished();
        }
        
    }

    //NOS ENCARGAMOS DE MOVER ARRIBA Y ABAJO POR LAS LINEAS DE DIALOGO
    void Navigation()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        //transform.GetChild(i).GetComponent<Text>().color = Color.blue;
        dialogueLines[dialogueLineIndex].color = Color.blue;
        if (activateDialogue)
        {
            if (vertical > 0)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    //transform.GetChild(i).GetComponent<Text>().color = Color.yellow;
                    dialogueLines[dialogueLineIndex].color = Color.yellow;
                    dialogueLineIndex--;
                    if (dialogueLineIndex < 0)
                        dialogueLineIndex = maxLineNumber - 1;
                }
            }
            else if (vertical < 0)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    //transform.GetChild(dialogueLineIndex).GetComponent<Text>().color = Color.yellow;
                    dialogueLines[dialogueLineIndex].color = Color.yellow;
                    dialogueLineIndex++;
                    if (dialogueLineIndex >= maxLineNumber)
                        dialogueLineIndex = 0;
                }
            }
            else
            {
                isMoving = false;
            }
        }

        //AL PRESIONAR ACCION SELECIONAMOS UNA LINEA DE DIALOGO. CONTROLAMOS QUE NO SE MANTENGA PULSADO EL BOTON NI QUE SE LE PUEDE DAR A ACCION MIENTRAS MUESTRE LA LINEA DE DIALOGO
        if (Input.GetAxisRaw("Action") != 0 && !holdKey && !talking)
        {
            if (!isAction)
            {
                isAction = true;
                //AL PRESIONAR ACCION SUMA 1 AL CONTADOR
                clickCounter++;
                
                //SI LA LINEA DE DIALOGO SELECCIONADA ES 2 O MENOR QUE EL NUMERO MÁXIMO DE LINEAS, ACCEDEMOS AL TIPO DE DIALOGO QUE SEA
                if (dialogueLineIndex >= 2 && dialogueLineIndex < maxLineNumber)
                {
                    DecideWichDialogue();
                }
                
                switch (clickCounter)
                {
                   
                    //LA PRIMERA VEZ QUE PULSAMOS, ACTIVAMOS LAS POSIBLES PREGUNTAS
                    //case 0:
                    //    if (i >= maxLineNumber - 1)
                    //    {
                    //        print("cuando entra aqui");
                    //        dialogueSetup.GetComponent<DialogueSetUp>().ExitDialogue();
                    //        transform.GetChild(i).GetComponent<Text>().color = Color.yellow;
                    //        ResponseManager.HideResponse();
                    //        i = 0;
                    //        index = -1;
                    //    }
                    //    //else
                    //    //{
                    //    //    ResponseManager.HideResponse();
                    //    //}
                    //    break;
                    //LA SEGUNDA VEZ QUE PULSAMOS SELECCIONAMOS UNA LINEA DE DIALOGO Y ESCONDEMOS LAS OTRAS POSIBLES PREGUNTAS
                    case 1:
                        if (dialogueLineIndex == maxLineNumber - 1 && (isinformation || wantToAsk))
                        {
                            print("case 1");
                            ResetLines();
                            FillDialogueLines(answerManager.FirstLevelDialogue);
                            transform.GetChild(dialogueLineIndex).GetComponent<Text>().color = Color.yellow;
                            dialogueLineIndex = 0;
                            isinformation = false;
                            wantToAsk = false;
                            clickCounter = 0;
                        }
                        else
                        {
                            if (dialogueLineIndex == maxLineNumber - 1)
                            {
                                print("case 1 else adios");
                                ResponseManager.ShowResponse(dialogueLineIndex, isinformation, wantToAsk, null);
                                line.GetComponent<Text>().text = "";
                                
                                dialogueSetup.GetComponent<DialogueSetUp>().ExitDialogue();
                                transform.GetChild(dialogueLineIndex).GetComponent<Text>().color = Color.yellow;
                                dialogueLineIndex = 0;
                                clickCounter = -1;
                            }
                            else
                            {
                                print("case 1 else");
                                AppearLine();
                                activateDialogue = false;
                                OnOFFDialogues(activateDialogue);
                            }
                        }
                        break;
                    //MOSTRAMOS LA RESPUESTA DEL NPC SI ESCOGEMOS LA ULTIMA LINEA (ADIOS) SALIMOS DEL DIALOGO
                    //case 2:
                    //    print("entra en el 2");
                    //    ResponseManager.ShowResponse(i, isinformation, wantToAsk, null);
                    //    line.GetComponent<Text>().text = "";
                    //    index = -1;
                    //    break;
                }
                holdKey = true;
                time = Time.time;
            }
        }
        else
            isAction = false;
    }

    void DecideWichDialogue()
    {
        //SI LA LINEA SELECCIONADA CORRESPONDE A NEED INFORMATION RELLENAMOS EL DIALOGOS CON LAS LINEAS DE GENERALUNIQUEQUESTIONS
        if(dialogueClass_Class.needInformation== dialogueLines[dialogueLineIndex].text) //transform.GetChild(dialogueLineIndex).GetComponent<Text>().text)
            isinformation = SelectDiferentDialogue(answerManager.GeneralUniqueQuestions);

        //SI LA LINEA SELECCIONADA CORRESPONDE A NEED INFORMATION RELLENAMOS EL DIALOGOS CON LAS LINEAS DE SPECIFICUNIQUEQUESTIONS
        if (dialogueClass_Class.wantToAsk == dialogueLines[dialogueLineIndex].text)
            wantToAsk = SelectDiferentDialogue(answerManager.SpecificUniqueQuestions);

        //SI LA LINEA SELECCIONADA CORRESPONDE AL MEOAN
        //
        if (dialogueClass_Class.playMeoan == dialogueLines[dialogueLineIndex].text)
        {
            dialogueLines[dialogueLineIndex].color = Color.yellow;
            ResponseManager.ShowResponse(dialogueLineIndex, isinformation, wantToAsk, null);
            line.GetComponent<Text>().text = "";

            dialogueSetup.GetComponent<DialogueSetUp>().ExitDialogue();
            //transform.GetChild(dialogueLineIndex).GetComponent<Text>().color = Color.yellow;
            //ResponseManager.HideResponse();
            //isMeoanOn = true;
            meoanSetup.SetUp();
            dialogueLineIndex = 0;
            clickCounter = -1;
            //clickCounter = 0;
            //dialogueLineIndex = maxLineNumber - 1;
        }
    }

    //RESETEAMOS LAS LINEAS, RELLENAMOS CON LAS NUEVAS, LAS PONEMOS EN AMARILLO, MARCAMOS LA PRIMERA Y RESETEAMOS EL COUNTER
    bool SelectDiferentDialogue(List<string> dialogue)
    {
        ResetLines();
        FillDialogueLines(dialogue);
        dialogueLines[dialogueLineIndex].color = Color.yellow;
        dialogueLineIndex = 0;
        clickCounter = 0;
        return true;
    }

    //COOLDOWN
    void Wait()
    {
        if (Time.time > time + 0.5)
        {
            holdKey = false;
        }
    }

    //VACIAMOS LAS LINEAS DE DIALOGO
    public void ResetLines()
    {
        for(int j=0;j< transform.childCount; j++)
        {
            transform.GetChild(j).GetComponent<Text>().text= "";
        }
    }
    
    //RELLENAMOS LAS LINEAS DE DIALOGO SEGUN EL ARRAYLIST QUE LE PASEMOS
    public void FillDialogueLines(List<string> array)
    {
        for (int j=0; j < array.Count; j++)
        {
            transform.GetChild(j).GetComponent<Text>().text = array[j];
        }
        maxLineNumber = array.Count;
        
    }

    //APARECEN LAS LETRAS UNA A UNA
    public void AppearLine()
    {
        talking = true;
        TextAppearanceManager textAppears = line.GetComponent<TextAppearanceManager>();
        textAppears.Text = line.GetComponent<Text>();
        textAppears.Sentence = dialogueLines[dialogueLineIndex].text.ToCharArray();
        textAppears.enabled = true;
        textAppears.DialogueScript = GetComponent<DialogueNavigation>();
       
    }

    public void LineFinished()
    {
        if (finishLine) {
            print("LineFinished");
            if (clickCounter!=-1)
            {
                ResponseManager.ShowResponse(dialogueLineIndex, isinformation, wantToAsk, GetComponent<DialogueNavigation>());
                line.GetComponent<Text>().text = "";
                clickCounter = -1;
                talking = true;
                finishLine = false;
            }
            else
            {
                if (dialogueLineIndex != maxLineNumber - 1)
                {
                    activateDialogue = true;
                    OnOFFDialogues(activateDialogue);
                    ResponseManager.HideResponse();
                    line.GetComponent<Text>().text = "";
                    talking = false;
                    finishLine = false;
                    clickCounter++;
                    print("el index suma "+ clickCounter);
                }
            }
        }
    }

    //ACTIVAMOS / DESACTIVAMOS LAS LINEAS DE DIALOGO
    public void OnOFFDialogues(bool isActive)
    {
        for (int j = 0; j < maxLineNumber; j++)
        {
            transform.GetChild(j).gameObject.SetActive(isActive);
        }
    }

    public ResponseManager ResponseManager
    {
        get
        {
            return responseManager;
        }

        set
        {
            responseManager = value;
        }
    }
    public int MaxLineNumber
    {
        get
        {
            return maxLineNumber;
        }

        set
        {
            maxLineNumber = value;
        }
    }
    public AnswerManager AnswerManager
    {
        get
        {
            return answerManager;
        }

        set
        {
            answerManager = value;
        }
    }

    public Dialogue_ConversationClass DialogueClass_Class
    {
        get
        {
            return dialogueClass_Class;
        }

        set
        {
            dialogueClass_Class = value;
        }
    }

    public bool IsMeoanOn
    {
        get
        {
            return isMeoanOn;
        }

        set
        {
            isMeoanOn = value;
        }
    }

    public bool FinishLine
    {
        get
        {
            return finishLine;
        }

        set
        {
            finishLine = value;
        }
    }
}

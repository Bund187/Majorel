using System.Collections;
using System.Collections.Generic;
using VIDE_Data;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_UIManager : MonoBehaviour {

    public GameObject clueIcon;
    public GameObject container_Player;
    public Text[] text_Choices;
    public DialogueSetupManager dialogueSetupScript;

    static List<string> foundedClues = new List<string>();

    List<string> cluesToFind = new List<string>();
    ClueManager clueScript;
    GameObject Container_NPC;
    Text textNpc;
    SayLineText npcSayLine;
    bool isAction, meoanIsOn;

    //// Use this for initialization
    void Start()
    {
        container_Player.SetActive(false);
        clueScript = GetComponent<ClueManager>();
    }

    private void Update()
    {
        //Si el dialogo esta activo y pulsamos accion, Next() se comunica con UpdateUI y muestrta la proxima linea de dialogo
        if (Input.GetAxisRaw("Action") != 0 && VD.isActive && !container_Player.activeSelf)
        {
            if (!isAction)
            {
                isAction = true;
                VD.Next();
            }
        }
        else
        {
            isAction = false;
        }
    }

    //Este metodo es el primero que se llama al empezar el dialogo
    public void Begin(VIDE_Assign vide_Assign, GameObject npc, GameObject npcTxt)
    {
        Container_NPC = npc;
        Container_NPC.SetActive(false);
        textNpc = npcTxt.GetComponent<Text>();
        npcSayLine = npcTxt.GetComponent<SayLineText>();
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(vide_Assign);
    }

    void UpdateUI(VD.NodeData data)
    {
        container_Player.SetActive(false);
        Container_NPC.SetActive(false);
       
        //Si el proximo nodo es de tipo player
        if (data.isPlayer) 
        {
            container_Player.SetActive(true);
            //Activamos solo tantas lineas de dialogo como haya y le damos el valor de texto del comment
            for(int i = 0; i < text_Choices.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    text_Choices[i].text = data.comments[i];
                    //print("Comentario a añadir: " + data.comments[i]);
                    if (data.comments[i] != "")
                    {
                      //  print("añadido " + i);
                        text_Choices[i].gameObject.SetActive(true);
                    }
                }
            }
        }
        else//Si el proximo nodo es de tipo npc accedemos al comment con indice hasta que llegue al max
        {
            Container_NPC.SetActive(true);
            string sentence = data.comments[data.commentIndex];

            //USAMOS $ PARA AÑADIR A LAS NOTAS EL EVENTO CORRESPONDIENTE
            if (sentence.Contains("$")){
                sentence=ClueSearch(sentence)[0];
            }
            textNpc.text = sentence;
            //npcSayLine.Talk(data.comments[data.commentIndex]);
            DeactivateDialogueLines();
        }
    }

    public string[] ClueSearch(string sentence)
    {
        bool clueFounded = false;
        string[] key = sentence.Split('$');

        for (int k = 0; k < foundedClues.Count; k++)
        {
            if (foundedClues[k].Equals(key[1]))
                clueFounded = true;
        }
        if (!clueFounded)
        {
            foundedClues.Add(key[1]);
            clueIcon.GetComponent<ClueIconBehaviour>().Temp = Time.time;
            clueIcon.SetActive(true);

            //LLAMAMOS AL SCRIPT QUE RELLENA LAS NOTAS
            clueScript.FillNotesWithClue(key[1]);
            Debug.Log("Se añadiría una pista al log");
        }

        return key;
    }

    void DeactivateDialogueLines()
    {
        for (int i = 0; i < text_Choices.Length; i++)
        {
            if(text_Choices[i]!=null)
                text_Choices[i].gameObject.SetActive(false);
        }
    }

    void End(VD.NodeData data)
    {
        if(container_Player!=null)
            container_Player.SetActive(false);
        Container_NPC.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
        DeactivateDialogueLines();
        if(!meoanIsOn)
            dialogueSetupScript.ExitDialogue();
    }

    //En caso de que le dialogo crashee llamamos al end
    private void OnDisable()
    {
        if (Container_NPC != null)
            End(null);
    }

    //Metodo que se llamará cuando el player clike sobre la respuesta deseada
    public void PlayerChoice(int choiceIndex)
    {
        VD.nodeData.commentIndex = choiceIndex;
        VD.Next();
    }

    public bool IsAction
    {
        get
        {
            return isAction;
        }

        set
        {
            isAction = value;
        }
    }

    public bool MeoanIsOn
    {
        get
        {
            return meoanIsOn;
        }

        set
        {
            meoanIsOn = value;
        }
    }
}

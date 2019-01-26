using System.Collections;
using System.Collections.Generic;
using VIDE_Data;
using UnityEngine;

public class DialogueSetupManager : MonoBehaviour
{
    public Dialogue_UIManager uiManager;
    public LoadXmlDialogues xmlScript;
    public DictionaryEvent dictionaryE;

    VIDE_Assign VIDE;
    GameObject[] dialogues;
    PlayerController playerScript;
    CameraCenterController CameraCenterController;
    
    public void Start()
    {
        CameraCenterController = Camera.main.GetComponent<CameraCenterController>();
        dialogues = new GameObject[transform.childCount];
        FillDialogues();
        
    }

    //RELLENA DIALOGUES[] CON LOS CHILDREN DE ESTE GAMEOBJECT
    void FillDialogues()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            dialogues[i] = transform.GetChild(i).gameObject;
        }

    }


    public void SetUpTextBox(GameObject npc, PlayerController player_Script)
    {
        playerScript = player_Script;
        string playerName = player_Script.gameObject.GetComponent<PlayerStats>().Stats1.Named;

        AssignDialogue(playerName);
        ChangeDialogues(playerName, npc.name);
        if (!VD.isActive)
        {
            uiManager.IsAction = true;
            if (!npc.name.Contains("Statue"))
                CameraCenter(true);
            uiManager.Begin(VIDE, npc.transform.GetChild(1).gameObject, npc.transform.GetChild(1).transform.GetChild(0).gameObject);
        }
    }

    //SEGUN EL NOMBRE DEL PLAYER SELECCIONAMOS UN DIALOGUE U OTRO
    void AssignDialogue(string playerName)
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i].name.Contains(playerName))
            {
                VIDE = dialogues[i].GetComponent<VIDE_Assign>();
            }
        }
    }

    //AQUI OCURRE LA MAGIA. RECORREMOS EL ARBOL DEL XML Y SI EXISTEN COMENTARIOS ESPECIFICOS PARA CADA PERSONAJE SE LOS AÑADIMOS
    void ChangeDialogues(string player, string npc)
    {
        for (int i=0;i< xmlScript.DClass.dialogueTypes.Count; i++)
        {
            if (xmlScript.DClass.dialogueTypes[i].name == VIDE.gameObject.name)
            {
                for (int j = 0; j < xmlScript.DClass.dialogueTypes[i].npc.Count; j++)
                {
                    if (xmlScript.DClass.dialogueTypes[i].npc[j].name == npc || xmlScript.DClass.dialogueTypes[i].npc[j].name =="All")
                    {
                        for (int k = 0; k < xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines.Count; k++)
                        {
                            if (xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines[k] != "")
                            {
                                VD.SetComment(VIDE.gameObject.name, k, 0, xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines[k]);
                            }
                        }

                    }else if(xmlScript.DClass.dialogueTypes[i].npc[j].name == player)
                    {
                        for (int k = 0; k < xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines.Count; k++)
                        {
                            if (xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines[k] != "")
                            {
                                if (xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines[k] != "")
                                {
                                    string[] comment = xmlScript.DClass.dialogueTypes[i].npc[j].dialogueLines[k].Split('_');
                                    //SI EL COMENTARIO ES ESPECIFICO O EXCLUENTE PARA UN NPC, BORRAMOS LA LINEA
                                    if(comment[1].Contains("/") || comment[1].Contains("%"))
                                    {
                                        VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, "");
                                    }

                                    //SI EL COMENTARIO TIENE UNA @ SIGNIFICA QUE ES UNA FRASE DE EVENTO.
                                    if (comment[1].Contains("@"))
                                    {
                                        string[] commentHaveKey = comment[1].Split('@');
                                        //SI EL COMENTARIO TIENE UN % HAY QUE EXCLUIR A UN PERSONAJE. SE CHEQUEA EL NOMBRE DEL PERSONAJE Y SI NO COINCIDE SE AÑADE EL COMENTARIO
                                        if (commentHaveKey[1].Contains("%"))
                                        {
                                            foreach (var item in dictionaryE.Events)
                                            {
                                                if (commentHaveKey[1].Contains(item.Key) && item.Value == true && !commentHaveKey[1].Contains(npc))
                                                {
                                                    VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, commentHaveKey[0]);
                                                }
                                            }
                                        }
                                        //DE NO HABER UN %,  SE CHEQUEA SI EL EVENTO ES EL MISMO DE ESTA FRASE Y SI ESTA TRUE, DE SER ASÍ SE INCLUYE
                                        else
                                        {
                                            foreach (var item in dictionaryE.Events)
                                            {
                                                if (item.Key == commentHaveKey[1] && item.Value == true)
                                                {
                                                    VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, commentHaveKey[0]);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //SI HAY UN / SOLO SE MOSTRARA ESE COMENTARIO PARA UN PERSONAJE EN CONCRETO. SI EL COMMENT COINCIDE CON EL NOMBRE DEL NPC SE AÑADE EL COMENTARIO
                                        if (comment[1].Contains("/"))
                                        {
                                            string[] specificComment = comment[1].Split('/');
                                            if (specificComment[1] == npc)
                                            {
                                                VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, specificComment[0]);
                                            }
                                        }
                                        else
                                        {
                                            if (comment[1].Contains("%"))
                                            {
                                                string[] specificComment = comment[1].Split('%');
                                                if (specificComment[1] != npc)
                                                {
                                                    VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, specificComment[0]);
                                                }
                                            }
                                            else
                                            {
                                                //AÑADIMOS EL COMENTARIO EN EL NODO CORRESPONDIENTE AL PLAYER. COMMENT[0] ES AL NUMERO OBTENIDO CON EL SPLIT Y CORRESPONDE AL NODO, LA K CORRESPONDE A LA POSICION DEL COMMENT
                                                VD.SetComment(VIDE.gameObject.name, int.Parse(comment[0]), k, comment[1]);
                                            }
                                        }
                                    }
                                    
                                    //print("Vide name "+ VIDE.gameObject.name + "Nodo " + int.Parse(comment[0]) + " Indice comentario " + k + " comentario " + comment[1]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void CameraCenter(bool center)
    {
        CameraCenterController.Talk = center;
        CameraCenterController.ActivateZoom = center;
    }

    public void ExitDialogue()
    {
        CameraCenter(false);
        playerScript.IsTalking = false;
    }
}

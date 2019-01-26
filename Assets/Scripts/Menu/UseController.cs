using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UseController : MonoBehaviour {

    public TextAppearanceManager textAppearanceManager;
    public PlayerController playerController;
    public GameObject inventory;
    public Text playerText;
    public GameObject[] colliderKill;
    public InventoryArrowBehaviour inventoryArrowBehaviour;
    public LoadXml_Misc loadXmlScript;

    private Sprite lastObjSelected;
    private int index;

    private void Awake()
    {
        index = 0;
    }

    //ESTO OCURRE AL PULSAR EL BOTON DE USAR
    public void Use()
    {
        bool killFind=false;
        //PONEMOS A FALSE EL BOOLEANO QUE CONTROLA EL INVENTARIO
        playerController.IsMenuOn = false;  

        //REPASAMOS TODOS LOS COLLIDERS EN LOS QUE SE PUEDE MATAR A UN NPC, SI KILLFIND ES TRUE SIGNIFICA QUE ESTAS EN CONTACTO CON UNO
        while(index<colliderKill.Length && !killFind)
        {
            //CUANDO SE ENCUENTRA EL QUE ESTA EN CONTACTO CON EL PLAYER SE PONE UN KILLFIND A TRUE
            if (colliderKill[index].GetComponent<DistantKillManager>().KillAvailable)
            {
                killFind = true;
            }
            else
                index++;
            
        }

        //SI EL BOOL ES FALSO O EL NOMBRE DEL COLLIDER NO ES IGUAL AL ULTIMO OBJETO SELECCIONADO EN EL INVENTARIO SIGNIFICA QUE O NO SE ESTÁ SOBRE EL COLLIDER O SE ESTÁ USANDO EL OBJETO ERRONEO
        if (!killFind || colliderKill[index].name!= lastObjSelected.name)
        {
            inventory.SetActive(playerController.IsMenuOn);
            Talk(loadXmlScript.MiscClass.cantUse);
        }
        //DE LO CONTRARIO SE SALE DEL MENU, SE ELIMINA EL OBJETO USADO Y SE EJECUTA LA FUNCION KILL() QUE HARA QUE SE MATE AL NPC
        else
        {
            inventory.SetActive(playerController.IsMenuOn);
            inventoryArrowBehaviour.RemoveInventoryObject(lastObjSelected);
            colliderKill[index].GetComponent<DistantKillManager>().Kill();
        }
        //inventory.SetActive(playerController.IsMenuOn);
        //inventoryArrowBehaviour.RemoveInventoryObject(lastObjSelected);
        //colliderKill[index].GetComponent<DistantKillManager>().Kill();

        index = 0;
    }

    //MUESTRA LA LINEA QUE DICE EL PERSONAJE
    private void Talk(string sentence)
    {
        textAppearanceManager.Text = playerText;
        textAppearanceManager.Sentence = sentence.ToCharArray();
        textAppearanceManager.enabled = true;
    }

    public Sprite LastObjSelected
    {
        get
        {
            return lastObjSelected;
        }

        set
        {
            lastObjSelected = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTextBoxBehaviour : MonoBehaviour {

    public UseController useScript;

    List<InventoryObjects> collection = new List<InventoryObjects>();

    //FUNCION LLAMADA POR EL CLICK (O PULSAR CON JOYSTICK) EN CUALQUIER OBJETO DEL INVENTARIO. LE PASAMOS DICHO OBJETO
    public void ObjectCollected(GameObject go)
    {
        //ALMACENAMOS EL SPRITE EN EL USE PARA QUE COMPRUEBE SI FUE EL ULTIMO OBJETO PULSADO
        Image goImg = go.GetComponent<Image>();
        useScript.LastObjSelected = goImg.sprite;
        print("objeto seleccionado " + goImg.sprite.name);

        //RECORREMOS EL LIST DE OBJETOS QUE SE HAN IDO COGIENDO O COMPRANDO Y CUANDO COINCIDE CON EL QUE HEMOS PULSADO DEL INVENTARIO MUESTRA LA DESCRIPCION
        for (int i =0; i < collection.Count; i++)
        {
            if (goImg.sprite.name == collection[i].inventorySprite.name)
            {
                ShowText(collection[i].Description);
            }
        }
    }

    void ShowText(string msg)
    {
        transform.GetChild(0).GetComponent<Text>().text = msg;
    }

    public List<InventoryObjects> Collection
    {
        get
        {
            return collection;
        }

        set
        {
            collection = value;
        }
    }
}

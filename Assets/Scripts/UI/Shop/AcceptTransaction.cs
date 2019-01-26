using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AcceptTransaction : MonoBehaviour {

    public Text totalGold,goldAmountShop, description;
    public GameObject goods;
    public GameObject noCoin, blur;
    public InventoryArrowBehaviour inventoryArrowBehaviour;
    public InventoryTextBoxBehaviour inventoryDescriptionScript;

    private InventoryObjects invObj;
    
    public void Accept()
    {
        //COMPROBAMOS QUE EL DINERO TOTAL NO SEA MENOR QUE EL PRECIO DEL OBJETO
        if (int.Parse(totalGold.text) >= invObj.price)
        {
            totalGold.text = (int.Parse(totalGold.text) - invObj.price).ToString();
            int i = 0;
            //DESTRUIMOS EL OBJETO COMPRADO. LO BUSCAMOS EN EL ARRAY Y LO DESTRUIMOS
            while (i < goods.transform.childCount)
            {
                if (invObj.shopSprite == goods.transform.GetChild(i).GetComponent<Image>().sprite)
                {
                    Destroy(goods.transform.GetChild(i).gameObject);
                    i = goods.transform.childCount;
                }
                i++;
            }
            //ACTUALIZAMOS LA CANTIDAD DE DINERO Y CERRAMOS EL PANEL DE COMPRA
            goldAmountShop.text = totalGold.text;
            transform.parent.gameObject.SetActive(false);
            blur.SetActive(false);

            //LE PASAMOS EL OBJETO A LA DESCRIPCION
            inventoryDescriptionScript.Collection.Add(invObj);

            //LE PASAMOS EL OBJETO A LOS SPRITES DEL INVENTARIO
            inventoryArrowBehaviour.Sprites.Add(invObj.inventorySprite);
            inventoryArrowBehaviour.ReorderInventory();
            description.text = "";
        }
        else
            noCoin.SetActive(true);
    }

    public InventoryObjects InvObj
    {
        get
        {
            return invObj;
        }

        set
        {
            invObj = value;
        }
    }
    
}

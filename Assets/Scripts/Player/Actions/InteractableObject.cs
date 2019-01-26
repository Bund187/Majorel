using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public GameObject menu;
    public GameObject[] collectableGameObjects;
    public Sprite[] spriteObjects;
    public Image addUIObjImg;
    public Text addUIObjTxt;
    public LoadXml_Misc loadXmlScript;
    public InventoryTextBoxBehaviour inventoryDescriptionScript;

    public GameObject goldText, goldImage;
    public Text goldAmount;

    protected InventoryArrowBehaviour InventoryArrowBehaviour;
    
    private void Awake()
    {
        InventoryArrowBehaviour = menu.GetComponent<InventoryArrowBehaviour>();
    }

    public void TakeObject(GameObject toCollect)
    {
        if (!toCollect.name.Contains("coin"))
        {
            for (int i = 0; i < collectableGameObjects.Length; i++)
            {
                if (toCollect == collectableGameObjects[i])
                {

                    addUIObjImg.sprite = toCollect.GetComponent<InventoryObjects>().inventorySprite;
                    addUIObjTxt.text = loadXmlScript.MiscClass.newObject;
                    addUIObjImg.gameObject.SetActive(true);
                    addUIObjTxt.gameObject.SetActive(true);
                    InventoryArrowBehaviour.Sprites.Add(toCollect.GetComponent<InventoryObjects>().inventorySprite);
                    inventoryDescriptionScript.Collection.Add(toCollect.GetComponent<InventoryObjects>());
                    print("To collect " + toCollect.name);
                    Destroy(toCollect);
                }
            }
            InventoryArrowBehaviour.ReorderInventory();
        }
        else
        {
            goldText.SetActive(true);
            goldImage.SetActive(true);
            goldAmount.text = (int.Parse(goldAmount.text) + 4).ToString();
            Destroy(toCollect);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryObjects : MonoBehaviour{

    public LoadXml_Misc loadXmlScript;
    public Sprite inventorySprite, shopSprite;
    public int price;

    protected string named;
    protected string description;

    private void Start()
    {
        switch (inventorySprite.name)
        {
            case "GiftBox":
                named = loadXmlScript.MiscClass.giftBox;
                description = loadXmlScript.MiscClass.giftBoxDescription;
                break;
            case "TrapSchematics":
                named = loadXmlScript.MiscClass.schematics;
                description = loadXmlScript.MiscClass.schematicsDescription;
                break;
            case "Flint&Steel":
                named = loadXmlScript.MiscClass.flintSteel;
                description = loadXmlScript.MiscClass.flintSteelDescription;
                break;
            case "Alcohol Bottle":
                named = loadXmlScript.MiscClass.alcoholBottle;
                description = loadXmlScript.MiscClass.alcoholBottleDescription;
                break;
            case "SnakeBox":
                named = loadXmlScript.MiscClass.snakeBox;
                description = loadXmlScript.MiscClass.snakeBoxDescription;
                break;

        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string Named
    {
        get
        {
            return named;
        }

        set
        {
            named = value;
        }
    }
}

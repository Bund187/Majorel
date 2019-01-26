using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowArticle : MonoBehaviour {

    public LoadXml_Misc loadXmlScript;

    public Image bigArticleImg;
    public Text confirmation, description;
    public AcceptTransaction acceptScript;

    public void ShowPanel(InventoryObjects invObject)
    {
        bigArticleImg.sprite = invObject.shopSprite;
        description.text = invObject.Description;
        confirmation.text = invObject.Named + "\n" + invObject.price + " " + loadXmlScript.MiscClass.gold + "\n" + loadXmlScript.MiscClass.shopConfirmation;
        acceptScript.InvObj = invObject;
    }
}

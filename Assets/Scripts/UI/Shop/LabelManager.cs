using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LabelManager : MonoBehaviour {

    public LoadXml_Misc loadXmlScript;
    public Text named, price;

	void Start () {
       
    }

    private void Update()
    {
        named.text = GetComponent<InventoryObjects>().Named;
        price.text = GetComponent<InventoryObjects>().price.ToString();
        if (transform.GetComponentInChildren<Text>().text != "")
        {
            this.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NoCoinTextAdder : MonoBehaviour {

    public LoadXml_Misc loadXmlScript;

	void Start () {
        GetComponent<Text>().text = loadXmlScript.MiscClass.notEnoughCoin;
	}
	
}

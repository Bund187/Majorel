using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpController : MonoBehaviour {

    public GameObject quincarnon;
    public TimeManager timeManager;

	void Update () {
	    if(timeManager.Hours==05 && timeManager.Minutes == 03)
        {
            quincarnon.SetActive(true);
        }	
	}
}

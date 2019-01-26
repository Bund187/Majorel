using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonController : MonoBehaviour {

    public PlayerController playerScript;
    public GameObject mapCamera, selector;
    public GameObject[] namesLockers;

    public void ClickMap()
    {
        playerScript.ToggleMenu();
        mapCamera.SetActive(true);
        selector.SetActive(true);
        playerScript.IsAttacking = true;
        for(int i=0; i< namesLockers.Length; i++)
        {
            namesLockers[i].SetActive(true);
        }
    }
}

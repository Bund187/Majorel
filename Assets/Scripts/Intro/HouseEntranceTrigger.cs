using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEntranceTrigger : MonoBehaviour {

    public GameObject[] deactivate;
    public GameObject activate, majorel;
    public ParentsDeathController parentsScript;
    public Narrator narratorScript;

    private void Update()
    {
        majorel.GetComponent<Animator>().SetBool("isMoving", true);
        if(deactivate[0].activeSelf)
            majorel.transform.position = Vector2.MoveTowards(majorel.transform.position, transform.position,Time.deltaTime*2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i=0; i < deactivate.Length; i++)
        {
            deactivate[i].SetActive(false);
        }
        activate.SetActive(true);
        parentsScript.enabled = true;
        narratorScript.CanContinue = true;
    }
}

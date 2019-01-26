using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour {

    public GameObject[] deactivateOpen, activateOpen;
    public GameObject[] npcDoor;
    public InsideHouseLayer insideHouseScript;
    //public HabitantSwitchOrder habitantOrderScript;

    private Animator anim;
    private SpriteRenderer spriteR;

	void Start () {
        anim = transform.parent.GetComponent<Animator>();
        spriteR = GetComponentInParent<SpriteRenderer>();
    }
	
    public void Open()
    {
        if (insideHouseScript != null)
        {
            insideHouseScript.HouseOpen = true;
            insideHouseScript.InhabitantsManager(insideHouseScript.HouseOpen);
        }
        spriteR.sortingOrder = 1;
        for(int i = 0; i<npcDoor.Length; i++)
        {
            npcDoor[i].SetActive(false);
        }
        //habitantOrderScript.HouseOpen = true;
        //habitantOrderScript.Reorder();
        anim.SetBool("open", true);
        foreach (GameObject deactivate in deactivateOpen)
        {
            deactivate.SetActive(false);
        }
        foreach (GameObject activate in activateOpen)
        {
            activate.SetActive(true);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (insideHouseScript != null)
            {
                insideHouseScript.HouseOpen = false;
                insideHouseScript.InhabitantsManager(insideHouseScript.HouseOpen);
            }
            spriteR.sortingOrder = 2;
            for (int i = 0; i < npcDoor.Length; i++)
            {
                npcDoor[i].SetActive(true);
            }
            //habitantOrderScript.HouseOpen = false;
           // habitantOrderScript.Reorder();
            transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            anim.SetBool("open", false);
            foreach (GameObject deactivate in deactivateOpen)
            {
                deactivate.SetActive(true);
            }
            foreach (GameObject activate in activateOpen)
            {
                if (activate!=null)
                    activate.SetActive(false);
            }
        }
    }
}

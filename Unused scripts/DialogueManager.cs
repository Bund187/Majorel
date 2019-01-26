using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject actionTrigger;
   
    GameObject npc;
    ActionTrigger actionT;
    Animator actionAnim;
    
    private void Awake()
    {
        actionT = actionTrigger.GetComponent<ActionTrigger>();
        actionAnim = actionTrigger.GetComponent<Animator>();
    }

    void Update () {
        if (Npc!=null) {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if ((vertical != 0 || horizontal != 0 || Input.anyKeyDown) && Npc.transform.GetChild(1).gameObject.activeSelf)
            {
                TextAppearanceManager textAppearance = Npc.transform.GetChild(1).gameObject.transform.GetComponentInChildren<TextAppearanceManager>();

                //textAppearance.ResetTextAppeareance(Npc.transform.GetChild(1).gameObject.GetComponentInChildren<Text>());
                Npc.transform.GetChild(1).gameObject.SetActive(false);
                actionT.IsDialogueOn = false;
                //textAppearance.IsActive = true;
                //actionTrigger.SetActive(true);

                if (vertical != 0 || horizontal != 0)
                {
                    actionT.OnOffAnimator(false);
                }
            }

        }
       
    }
    
    public void DialogueSwitch()
    {
        actionT.IsDialogueOn = true;
        Npc.transform.GetChild(1).gameObject.transform.GetComponentInChildren<TextAppearanceManager>().DialogueIndex++;
        Npc.transform.GetChild(1).gameObject.SetActive(true);
    }


    public GameObject Npc
    {
        get
        {
            return npc;
        }

        set
        {
            npc = value;
        }
    }
}

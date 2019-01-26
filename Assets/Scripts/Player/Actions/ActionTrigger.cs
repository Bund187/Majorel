using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public GameObject collection, characterSelection, charImageChanger, dialogue, combatIcon, meoan;
    public RuntimeAnimatorController talk, action;

    bool isAction;
    GameObject goInteract;
    bool isDialogueOn, isTouching;
    Animator anim;
    SpriteRenderer spriteR;
    InteractableObject InteractableObject;
    CharacterSelectionManager CharacterSelectionManager;
    CharacterSelectionImageDispatcher CharacterSelectionImageDispatcher;
    DialogueSetUp DialogueSetUp;
    PlayerController playerController;

    DialogueSetupManager newDialogueTestScript;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        InteractableObject = collection.GetComponent<InteractableObject>();
        CharacterSelectionManager = characterSelection.GetComponent<CharacterSelectionManager>();
        CharacterSelectionImageDispatcher = charImageChanger.GetComponent<CharacterSelectionImageDispatcher>();
        DialogueSetUp = dialogue.GetComponent<DialogueSetUp>();
        newDialogueTestScript = dialogue.GetComponent<DialogueSetupManager>();
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (isTouching)
            Interact();
    }

    public void Interact()
    {
        if (Input.GetAxisRaw("Action") != 0)
        {
            if (!isAction && goInteract != null && isTouching && !playerController.IsMenuOn && !meoan.activeSelf)
            {
                isAction = true;

                switch (goInteract.tag)
                {
                    case "Door":
                        goInteract.GetComponent<DoorAction>().Open(); ;
                        break;
                    case "Action":
                        goInteract.GetComponent<ActionManager>().PerformAction(); ;
                        break;
                    case "Take":
                        InteractableObject.TakeObject(goInteract);
                        break;
                    case "NPC":
                        ////ESTA INFO DE CAMBIO DE PERSONAJE DEBE OCURRIR CUANDO EL NPC MUERE. 
                        //CharacterSelectionManager.CharStats[goInteract.GetComponent<PlayerStats>().Stats1.Index] = goInteract.GetComponent<PlayerStats>().Stats1;
                        //CharacterSelectionImageDispatcher.SpriteAdder(goInteract.GetComponent<PlayerStats>().Stats1.Index);
                        //playerController.gameObject.GetComponent<PlayerStats>().Stats1 = goInteract.GetComponent<PlayerStats>().Stats1;
                        ////
                        //SISTEMA DE DIALOGOS
                        if (!playerController.IsTalking)
                        {
                            //AÑADIMOS EL LISTENER Y EL SPEAKER AL SETUP DEL DIALOGO Y LO INICIALIZAMOS
                            //if(goInteract.GetComponent<PathFind>()!=null)
                            //    goInteract.GetComponent<PathFind>().enabled = false;

                            playerController.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                            //DialogueSetUp.Listener = goInteract;
                            //DialogueSetUp.Speaker = transform.parent.gameObject;
                            //DialogueSetUp.SetUpTextBox();
                            //transform.parent.GetComponent<Animator>().SetBool("isMoving", false);
                            newDialogueTestScript.SetUpTextBox(goInteract,playerController);
                            playerController.IsTalking = true;
                            //isTouching = false;
                        }
                        break;
                }
                OnOffAnimator(false);
            }
        }
        else
        {
            isAction = false;
        }
    }

    public void OnOffAnimator(bool active)
    {
        anim.enabled = active;
        spriteR.enabled = active;
        if (active) anim.Play(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Action") || collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Take") || collision.gameObject.tag.Equals("Shop"))
        {
            isTouching = true;
            if (collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Shop"))
            {
                anim.runtimeAnimatorController = talk;
            }
            else
            {
                anim.runtimeAnimatorController = action;
            }
            OnOffAnimator(true);
            goInteract = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Action") || collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Take") || collision.gameObject.tag.Equals("Shop"))
        {
            print("sale del colider");
            OnOffAnimator(false);
            if (collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Shop"))
            {
                combatIcon.SetActive(false);
            }
            isTouching = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Door") || (collision.gameObject.tag.Equals("Action")))
        {
            isTouching = true;
            anim.runtimeAnimatorController = action;
            OnOffAnimator(true);
            goInteract = collision.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Door")|| (collision.gameObject.tag.Equals("Action")))
        {
            OnOffAnimator(false);
            isTouching = false;
        }
    }


    public GameObject GoInteract
    {
        get
        {
            return goInteract;
        }

        set
        {
            goInteract = value;
        }
    }

    public bool IsDialogueOn
    {
        get
        {
            return isDialogueOn;
        }

        set
        {
            isDialogueOn = value;
        }
    }

    public bool IsTouching
    {
        get
        {
            return isTouching;
        }

        set
        {
            isTouching = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTrapController : ActionManager {

    public SayLineText sayLineScript;
    public LoadXml_Misc loadXml;
    public RightPlaceController[] rightPlaceScripts;
    public SayLineText saylineScript;
    public GameObject canvasTrap, killRosita;
    public SayLineText majorelSayLineAScript;
    public Sprite trapArmedSprite;
    public PlayerController playerScript;
    public InteractableObject interactScript;
    public InventoryArrowBehaviour inventoryScript;
    public Sprite emptyBox;
    public DictionaryEvent dictionaryEvent;

    bool haveSchematics,trapArmed, textAppearEnabled,snakeCaught, haveBox, finish;
    SpriteRenderer sprRend;
    WaitBehaviour waitScript;
    int seconds;

    private void Awake()
    {
        seconds = 0;
        sprRend = GetComponent<SpriteRenderer>();
        textAppearEnabled = saylineScript.gameObject.GetComponent<TextAppearanceManager>().enabled;
        waitScript = new WaitBehaviour();
    }


    private void Update()
    {
        if(trapArmed && !saylineScript.gameObject.GetComponent<TextAppearanceManager>().enabled && playerScript.IsMenuOn && !finish)
        {
            canvasTrap.SetActive(false);
            playerScript.IsMenuOn = false;
            sprRend.sprite = trapArmedSprite;
            waitScript.Time = Time.time;
            seconds = 5;
            finish = true;
        }
        
        if (waitScript.Wait(seconds))
        {
            snakeCaught = true;
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    interactScript.TakeObject(transform.GetChild(0).gameObject);
        //    inventoryScript.RemoveInventoryObject(emptyBox);
        //    killRosita.SetActive(true);
        //}

    }
    //CHEKEAMOS LA TRAMPA CADA VEZ QUE SE COLOCA UNA PIEZA
    public void CheckTrap()
    {
        if (!trapArmed)
        {
            int i = 0;
            bool empty = false;
            //RECORREMOS EL ARRAY DE PIEZAS COLOCADAS, SI ALGUNA NO HA SIDO PUESTA OPNEMOS EMPTY A TRUE
            while (i < rightPlaceScripts.Length && !empty)
            {
                if (rightPlaceScripts[i].correctPlace != 1)
                {
                    empty = true;
                }
                i++;
            }
            //SI EMPTY ES FALSE QUIERE DECIR QUE TODAS HAN SIDO COLOCADAS CORRECTAMENTE POR LO TANTO SE INICIA EL FINAL
            if (!empty)
            {
                saylineScript.Talk(loadXml.MiscClass.armedTrap);
                trapArmed = true;
            }
        }
    }

    //SE EJECUTA AL COLISIONAR CON EL COLIDER E INTERACTUAR
    public override void PerformAction()
    {
        dictionaryEvent.Events["trap"] = true;

        //CUANDO NO SE TIENEN LOS ESQUEMAS DIRA QUE HAY UNA TRAMPA DESMONTADA
        if (!haveSchematics)
        {
            sayLineScript.Talk(loadXml.MiscClass.disarmedTrap);
        }
        else
        {
            //SE TIENE EL ESQUEMA PERO LA TRAMPA NO ESTÁ ARMADA POR LO QUE PASAREMOS A MONTARLA.
            if (!trapArmed)
            {
                playerScript.IsMenuOn = true;
                canvasTrap.SetActive(true);
            }
            else
            {
                //CUANDO SE HAYA MONTADO LA TRAMPA SI AUN NO HA PASADO EL TIEMPO PARA QUE COJA LA SERPIENTE DIRA UNA FRASE
                if (!snakeCaught)
                    majorelSayLineAScript.Talk(loadXml.MiscClass.snakeNoBoxSentence);
                //CUANDO HAYA PASADO EL TIEMPO SE CONTROLARA SI SE TIENE LA CAJA
                else
                {
                    if(!haveBox)
                        majorelSayLineAScript.Talk(loadXml.MiscClass.snakeNoBoxSentence);
                    else
                    {
                        majorelSayLineAScript.Talk(loadXml.MiscClass.snakeBoxSetence);
                        interactScript.TakeObject(transform.GetChild(0).gameObject);
                        inventoryScript.RemoveInventoryObject(emptyBox);
                        killRosita.SetActive(true);
                        this.enabled = false;
                    }

                }
            }
        }
    }

    public void ExitTrap()
    {
        playerScript.IsMenuOn = false;
    }

    public bool HaveSchematics
    {
        get
        {
            return haveSchematics;
        }

        set
        {
            haveSchematics = value;
        }
    }

    public bool HaveBox
    {
        get
        {
            return haveBox;
        }

        set
        {
            haveBox = value;
        }
    }
}

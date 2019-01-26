using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionBehaviour : MonoBehaviour
{
    public Text charName, level, health, strenght, speed, description;
    public GameObject player, characterSelection, menu;
    public GameObject[] doorsToOpen;
    public GameObject[] doorsToClose;
    public TransmutationBlocker transmutationBlockScript;
    public SayLineText sayLineScript;
    public LoadXml_Misc loadXml;
    Animator anim;
    Image thisImage;
    
    private void Start()
    {
        thisImage = GetComponent<Image>();
    }

    public void CharacterSelect(string charIndex)
    {
        if (!transmutationBlockScript.TransmutationBlock)
        {
            int i = int.Parse(charIndex);

            StatsManager.Stats stats = characterSelection.GetComponent<CharacterSelectionManager>().CharStats[i];

            description.text = stats.Description;
            charName.text = stats.Named;
            //level.text = stats.Level.ToString();
            //health.text = stats.Health.ToString();
            //strenght.text = stats.Strength.ToString();
            //speed.text = stats.Speed.ToString();

            if (stats.Named != "?")
            {
                player.GetComponent<PlayerStats>().Stats1 = stats;
                player.GetComponent<Animator>().runtimeAnimatorController = stats.Anim;
            }
            print("CharacterSelection Stats name " + stats.Named);
            for (int j = 0; j < doorsToClose.Length; j++)
            {
                doorsToClose[j].SetActive(true);
                doorsToOpen[j].SetActive(false);

                if (doorsToClose[j].name.Contains(charName.text))
                {
                    doorsToClose[j].SetActive(false);
                    doorsToOpen[j].SetActive(true);
                }
            }
        }
        else
        {
            menu.SetActive(false);
            player.GetComponent<PlayerController>().IsMenuOn = false;
            sayLineScript.Talk(loadXml.MiscClass.cantTransmutate);
        }
        
    }
    public void SkinPicker(GameObject obj)
    {
        if (!transmutationBlockScript.TransmutationBlock)
        {
            Sprite currentImg = obj.GetComponent<Image>().sprite;
            thisImage.sprite = currentImg;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }
}

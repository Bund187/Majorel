using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFarmerBurnt : DistantKillManager {

    public AldisPathFind aldisPathScript;
    public Animator strawAnim;
    public CharacterSelectionManager characterSelectionManager;
    public CharacterSelectionImageDispatcher characterSelectionImageDispatcher;
    public PlayerController playerScript;

    private bool toDestroy;

    private void Update()
    {
        if (toDestroy)
        {
            if(killed.GetComponent<SpriteRenderer>().sprite.name== "Aldis_Burning_62" && !aldisPathScript.enabled) { 
            
                RecentKilledText("Aldis");
                killIcon.SetActive(false);
                deadPower.SetActive(true);
                Destroy(killed);
                Destroy(this);
            }
        }
    }

    public override void Kill()
    {
        strawAnim.SetTrigger("burn");
        anim.SetTrigger("burn");
        characterSelectionManager.CharStats[killed.GetComponent<PlayerStats>().Stats1.Index] = killed.GetComponent<PlayerStats>().Stats1;
        characterSelectionImageDispatcher.SpriteAdder(killed.GetComponent<PlayerStats>().Stats1.Index);
        aldisPathScript.enabled = false;
        playerScript.IsTalking = true;
        toDestroy = true;
    }
}

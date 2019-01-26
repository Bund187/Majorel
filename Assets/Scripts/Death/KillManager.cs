using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour {

    public GameObject killIcon;
    public GameObject killed, deadPower;
    public CharacterSelectionManager characterSelectionManager;
    public CharacterSelectionImageDispatcher characterSelectionImageDispatcher;
    public RecentKilledController recentKillScript;
    public PlayerController playerScript;

    protected Animator anim;
    protected GameObject player;
    protected GameObject kill;
    
    private void Start()
    {
       anim = killed.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            kill = this.gameObject;
            player = collision.transform.parent.gameObject;
            killIcon.SetActive(true);
            if(!playerScript.IsTalking && !playerScript.IsMenuOn)
                if (Input.GetAxisRaw("Cancel") != 0)
                    KillNPC();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            killIcon.SetActive(false);
        }
    }

    protected virtual void KillNPC()
    {
       
    }

    protected void RecentKilledText(string npcName)
    {
        recentKillScript.NpcName = npcName;
        recentKillScript.IsFading = false;
        recentKillScript.enabled = true;
        recentKillScript.WaitScript.Time = Time.time;
    }
}

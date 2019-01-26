using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantKillManager : MonoBehaviour {

    public GameObject killIcon, deadPower;
    public RecentKilledController recentKillScript;
    public GameObject killed;

    protected Animator anim;

    bool killAvailable;

    private void Start()
    {
        if(killed!=null)
            anim = killed.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            killAvailable = true;
            killIcon.SetActive(killAvailable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            killAvailable = false;
            killIcon.SetActive(killAvailable);
        }
    }

    public virtual void Kill()
    {

    }

    protected void RecentKilledText(string npcName)
    {
        recentKillScript.NpcName = npcName;
        recentKillScript.IsFading = false;
        recentKillScript.enabled = true;
        recentKillScript.WaitScript.Time = Time.time;
    }

    public bool KillAvailable
    {
        get
        {
            return killAvailable;
        }

        set
        {
            killAvailable = value;
        }
    }
}

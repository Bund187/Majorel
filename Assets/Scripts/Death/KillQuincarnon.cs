using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuincarnon : KillManager
{

    public QuincarnonBlockDate blockDateString;
    public LoadXml_Misc loadXmlScript;
    public Animator majorelAnim;
    public GameObject demo;
    string playerName;
    bool toDestroy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            majorelAnim.SetTrigger("attack");
        }

        if (toDestroy)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.815f && !anim.IsInTransition(0))
            {
                blockDateString.enabled = false;
                RecentKilledText("Quincarnon");
                Destroy(killed);
                killIcon.SetActive(false);
                demo.SetActive(true);
                Destroy(this);
            }
        }
    }

    protected override void KillNPC()
    {
        if (playerName.Contains("Rosita"))
        {
            PlayerStats playerStatsScript = killed.GetComponent<PlayerStats>();
            majorelAnim.SetTrigger("attack");
            anim.SetTrigger("isDead");
            characterSelectionManager.CharStats[playerStatsScript.Stats1.Index] = playerStatsScript.Stats1;
            characterSelectionImageDispatcher.SpriteAdder(playerStatsScript.Stats1.Index);
            toDestroy = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerName = collision.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite.name;
            print("playername " + playerName);
            if (playerName.Contains("Rosita"))
            {
                blockDateString.SayLine(loadXmlScript.MiscClass.quincarnonLate);
                blockDateString.WaitTime = 10;
            }
            else
            {
                blockDateString.GetAway(true);
            }
        }
    }
}

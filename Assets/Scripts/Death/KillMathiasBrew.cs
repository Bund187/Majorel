using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMathiasBrew : DistantKillManager
{

    public CharacterSelectionManager characterSelectionManager;
    public CharacterSelectionImageDispatcher characterSelectionImageDispatcher;
    public PlayerController playerScript;
    public Animator anima;
    public ActionTrigger actionScript;

    private bool toDestroy;

    private void Update()
    {
        if (toDestroy)
        {
            if (anima.gameObject.GetComponent<SpriteRenderer>().sprite.name == "MathiasDead_39")
            {
                //if (anima.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.815f && !anima.IsInTransition(0))
                //{
                RecentKilledText("Mathias Brew");
                killIcon.SetActive(false);
                deadPower.SetActive(true);
                //Destroy(killed);

                //Destroy(this);
                GetComponent<PolygonCollider2D>().enabled = false;
                this.enabled = false;
            }
        }
    }

    public override void Kill()
    {
        actionScript.OnOffAnimator(false);
        anima.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        anima.SetTrigger("kill");
        characterSelectionManager.CharStats[killed.GetComponent<PlayerStats>().Stats1.Index] = killed.GetComponent<PlayerStats>().Stats1;
        characterSelectionImageDispatcher.SpriteAdder(killed.GetComponent<PlayerStats>().Stats1.Index);
        playerScript.IsTalking = true;
        toDestroy = true;
    }
}

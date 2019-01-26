using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMathias : KillManager {

    bool finishAnim;
    
    private void Update()
    {
        if (finishAnim)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.815f && !anim.IsInTransition(0))
            {
                //GetComponent<BoxCollider2D>().enabled = false;
                killIcon.SetActive(false);
                player.GetComponent<PlayerController>().IsAttacking = false;
                Destroy(this);
            }
        }
    }

    protected override void KillNPC()
    {
        anim = GetComponent<Animator>();
        if (!finishAnim)
        {
            RecentKilledText("Mathias Brew");
            //recentKillScript.NpcName = "Mathias Brew";
            //recentKillScript.IsFading = false;
            //recentKillScript.enabled = true;
            //recentKillScript.WaitScript.Time = Time.time;
            player.GetComponent<PlayerController>().IsAttacking = true;
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            if (playerSprite.flipX)
                playerSprite.flipX = false;
            Vector2 targetPosition = new Vector2(2.607f, -15.325f);
            Animator playerAnim = player.GetComponent<Animator>();
            player.transform.position = targetPosition;
            playerAnim.SetTrigger("killMathias");
            anim.SetTrigger("kill");
            characterSelectionManager.CharStats[killed.GetComponent<PlayerStats>().Stats1.Index] = killed.GetComponent<PlayerStats>().Stats1;
            characterSelectionImageDispatcher.SpriteAdder(killed.GetComponent<PlayerStats>().Stats1.Index);
            finishAnim = true;
        }
    }
}

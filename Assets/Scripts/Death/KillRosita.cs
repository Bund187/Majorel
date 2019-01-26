using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRosita : KillManager {

    bool toDestroy;

    private void Update()
    {
        if (toDestroy)
        {
            if (anim.gameObject.GetComponent<SpriteRenderer>().sprite.name == "Rosita_Dead_28")
            {
                Destroy(killed);
                Destroy(this);
                deadPower.SetActive(true);
                playerScript.IsTalking = false;
            }
        }
    }

    protected override void KillNPC()
    {
        playerScript.IsTalking = true;
        RecentKilledText("Rosita");
        PlayerStats playerStatsScript = killed.GetComponent<PlayerStats>();
        anim.SetTrigger("isDead");
        characterSelectionManager.CharStats[playerStatsScript.Stats1.Index] = playerStatsScript.Stats1;
        characterSelectionImageDispatcher.SpriteAdder(playerStatsScript.Stats1.Index);
        toDestroy = true;
    }
}

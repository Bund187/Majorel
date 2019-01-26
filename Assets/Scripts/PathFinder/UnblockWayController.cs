using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockWayController : MonoBehaviour {

    public PlayerController playerScript;
    public ActionTrigger actionTriggerScript;

    private int counter, exitCounter;

    private void Awake()
    {
        counter = 0;
        exitCounter = 0;
    }

    private void Update()
    {
        if (!GetComponent<Collider2D>().enabled)
        {
            counter = 0;
            exitCounter++;
            if (exitCounter >= 100)
                GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!playerScript.IsTalking)
        {
            if (collision.gameObject.tag == "Player" && playerScript.IsMoving)
            {
                exitCounter = 0;
                counter++;
                if (counter >= 100)
                {
                    GetComponent<Collider2D>().enabled = false;
                    actionTriggerScript.OnOffAnimator(false);
                    actionTriggerScript.IsTouching = false;
                    GetComponent<PathFind>().IsColliding = false;
                }
            }
        }
    }
    
    
}

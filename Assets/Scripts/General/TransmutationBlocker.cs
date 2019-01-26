using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationBlocker : MonoBehaviour {

    bool transmutationBlock;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            transmutationBlock = true;
            print(transmutationBlock + " " + collision.gameObject + " está blockeando la transmutación");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            transmutationBlock = false;
            print(transmutationBlock+ " " + collision.gameObject + " ya NO blokea la transmutación");
        }
    }
    public bool TransmutationBlock
    {
        get
        {
            return transmutationBlock;
        }

        set
        {
            transmutationBlock = value;
        }
    }
}

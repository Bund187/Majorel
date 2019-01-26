using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeactivator : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
    }
}

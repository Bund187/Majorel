using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLayerOrder : MonoBehaviour {

    public bool priorityUp, priorityDown, priorityLeft, priorityRight,touching, touchingOrderUp, touchingOrderDown;

    SpriteRenderer spriteRend;
    int order;
    GameObject player;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        order = spriteRend.sortingOrder;
        player = GameObject.Find("Majorel");
    }

    void Update () {
        NewOrder();
	}

    public void NewOrder()
    {
        SpriteRenderer playerSpriteR = player.gameObject.GetComponent<SpriteRenderer>();
        if (priorityUp)
        {
            priorityDown = false;
            if (transform.position.y > player.transform.position.y)
            {
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            }
            else
            {
                spriteRend.sortingOrder = order;
            }
        }
        if (priorityDown)
        {
            priorityUp = false;
            if (transform.position.y < player.transform.position.y)
            {
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            }
            else
            {
                spriteRend.sortingOrder = order;
            }
        }

        if (priorityLeft)
        {
            priorityRight = false;
            if (transform.position.x < player.transform.position.x)
            {
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            }
            else
            {
                spriteRend.sortingOrder = order;
            }
        }
        if (priorityRight)
        {
            priorityLeft = false;
            if (transform.position.x > player.transform.position.x)
            {
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            }
            else
            {
                spriteRend.sortingOrder = order;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer playerSpriteR = player.gameObject.GetComponent<SpriteRenderer>();
        if (touching && (collision.tag.Equals("Player") || collision.tag.Equals("NPC")))
        {
            if(touchingOrderUp)
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            else
                spriteRend.sortingOrder = order;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SpriteRenderer playerSpriteR = player.gameObject.GetComponent<SpriteRenderer>();
        if (touching && (collision.tag.Equals("Player") || collision.tag.Equals("NPC")))
        {
            if (touchingOrderUp)
                spriteRend.sortingOrder = order;
            else
                spriteRend.sortingOrder = playerSpriteR.sortingOrder+1;
            
        }
    }
}

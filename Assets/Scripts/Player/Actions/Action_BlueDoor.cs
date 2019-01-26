using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BlueDoor : ActionManager
{
    public Sprite open, close;
    public GameObject door;

    private SpriteRenderer spriteR;
    private bool doorOpen;
    private PolygonCollider2D polygonCol;

    private void Awake()
    {
        polygonCol = GetComponent<PolygonCollider2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    public override void PerformAction()
    {
        polygonCol.enabled = doorOpen;
        doorOpen = !doorOpen;
        if (doorOpen)
            spriteR.sprite = open;
        else
            spriteR.sprite = close;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Untagged")
        {
            doorOpen = !doorOpen;

            polygonCol.enabled = false;
            spriteR.sprite = open;
            if (door != null)
            {
                door.SetActive(true);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Untagged")
        {
            doorOpen = !doorOpen;

            polygonCol.enabled = true;
            spriteR.sprite = close;

            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }

}

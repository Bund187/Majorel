using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseLayer : MonoBehaviour {

    private Dictionary<string, GameObject> inhabitants = new Dictionary<string, GameObject>();
    private bool houseOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag.Equals("Player"))
        //{
        //    houseOpen = true;
        //    InhabitantsManager(houseOpen);
        //}
        if(collision.tag.Equals("NPC"))
        {
            if (!houseOpen)
            {
                Reorder(collision.gameObject, 0, false);
            }
            if (!inhabitants.ContainsKey(collision.gameObject.name))
            {
                //print("Ha entrado a la casa " + collision.gameObject.name);
                inhabitants.Add(collision.gameObject.name, collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag.Equals("Player"))
        //{
        //    houseOpen = false;
        //    InhabitantsManager(houseOpen);
        //}
        if (collision.tag.Equals("NPC"))
        {
            Reorder(collision.gameObject, 3, true);
            inhabitants.Remove(collision.gameObject.name);
            //print("Ha salido de la casa " + collision.gameObject.name);
        }
    }

    private void Reorder(GameObject npc, int order, bool isIn)
    {
        npc.GetComponent<PositionLayerOrder>().enabled = isIn;
        npc.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

    public void InhabitantsManager(bool open)
    {
        List<GameObject> npcs = new List<GameObject>(inhabitants.Values);

        if (open)
        {

            foreach (GameObject npc in npcs)
            {
                Reorder(npc, 3, true);
            }
        }
        else
        {
            foreach (GameObject npc in npcs)
            {
                Reorder(npc, 0, false);
            }
        }
    }

    public bool HouseOpen
    {
        get
        {
            return houseOpen;
        }

        set
        {
            houseOpen = value;
        }
    }
}

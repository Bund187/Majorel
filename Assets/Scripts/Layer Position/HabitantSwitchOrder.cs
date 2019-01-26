using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantSwitchOrder : MonoBehaviour {

    //private List<NpcLayerOrderManager> npcOrderScript = new List<NpcLayerOrderManager>();
    private bool houseOpen;
    private Dictionary<string, bool> habitantsInside = new Dictionary<string, bool>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("NPC"))
        {
            habitantsInside.Add(collision.name, true);
            NpcLayerOrderManager npcOrderScript = collision.GetComponent<NpcLayerOrderManager>();
            npcOrderScript.SpriteR = collision.gameObject.GetComponent<SpriteRenderer>();
            npcOrderScript.insideHouse = true;
            npcOrderScript.houseOpen = houseOpen;
            npcOrderScript.Reorder();
            print("Entra " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("NPC"))
        {
            NpcLayerOrderManager npcOrderScript = collision.GetComponent<NpcLayerOrderManager>();
            npcOrderScript.insideHouse = false;
            npcOrderScript.Reorder();
            print("Sale " + collision.gameObject.name);
        }
    }

    public void Reorder()
    {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLayerOrderManager : MonoBehaviour {

    public int normalOrder, inOrder;
    public bool insideHouse, houseOpen;
    SpriteRenderer spriteR;

    public void Reorder()
    {
        PositionLayerOrder positionScript = spriteR.gameObject.GetComponent<PositionLayerOrder>();
        if (!houseOpen && InsideHouse)
        { // Si majorel NO esta en la casa pero el npc si que está
            spriteR.sortingOrder = inOrder;
            positionScript.enabled = false;
        }
        else if (!houseOpen && !insideHouse)
        { //Si majorel no está en la casa y el npc tampoco
            spriteR.sortingOrder = normalOrder;
            positionScript.enabled = true;
        }
        else if (houseOpen && InsideHouse)
        { // Si majorel esta en la casa y el npc también
            //print("Majorel entra y el layer cambia para " + spriteR.gameObject.name);
            spriteR.sortingOrder = normalOrder;
            positionScript.enabled = true;
        }
    }

    public bool InsideHouse
    {
        get
        {
            return insideHouse;
        }

        set
        {
            insideHouse = value;
        }
    }

    public SpriteRenderer SpriteR
    {
        get
        {
            return spriteR;
        }

        set
        {
            spriteR = value;
        }
    }
}

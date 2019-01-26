using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryArrowBehaviour : MonoBehaviour {

    public GameObject[] inventorySets;
    public DoorDialogue doorDialogue;
    public SnakeTrapController snakeTrapScript;
    public Sprite emptySlot;

    float waitingSecs;
    Image thisImage;
    List<Sprite> sprites = new List<Sprite>();
    List<GameObject> boxes = new List<GameObject>();
    int index;

    private void Awake()
    {
        index = 0;
        thisImage = GetComponent<Image>();
        waitingSecs = 0.05f;
    }
    
	public void ButtonPressed()
    {
        print("+index " + index);
        if (index>= inventorySets.Length-1)
            index = 0;
        else
            index++;
        for(int i = 0; i < inventorySets.Length; i++)
        {
            if (i != index)
            {
                inventorySets[i].SetActive(false);
                for (int j = 0; j < inventorySets[i].transform.childCount; j++)
                {
                    for (int k = 0; k < inventorySets[i].transform.GetChild(j).transform.childCount; k++)
                    {
                        inventorySets[i].transform.GetChild(j).transform.GetChild(k).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                inventorySets[i].SetActive(true);
                for(int j = 0; j < inventorySets[i].transform.childCount; j++)
                {
                    for(int k = 0; k < inventorySets[i].transform.GetChild(j).transform.childCount; k++)
                    {
                        StartCoroutine(ObjectShow(inventorySets[i].transform.GetChild(j).gameObject));
                    }
                }
            }
        }
       
    }
    //SE ELIMINA EL OBJETO USADO, SE PONEN COMO "VACIOS" TODOS LOS SLOTS DEL INVENTARIO Y SE REORDENA EL INVENTARIO CON EL LIST DE SPRITES QUE CONTIENEN LOS OBJETOS OBTENIDO
    public void RemoveInventoryObject(Sprite lastObjSelected)
    {
        Sprites.Remove(lastObjSelected);
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].GetComponent<Image>().sprite = emptySlot;
        }
        ReorderInventory();
    }

    public void ClearBoxes()
    {
        for (int l = 0; l < inventorySets.Length; l++)
        {
            for (int j = 0; j < inventorySets[l].transform.childCount; j++)
            {
                for (int k = 0; k < inventorySets[l].transform.GetChild(j).transform.childCount; k++)
                {
                    boxes.Add(inventorySets[l].transform.GetChild(j).transform.GetChild(k).gameObject);
                }
            }
        }
    }

    public void ReorderInventory()
    {
        ClearBoxes();
        for (int i = 0; i < sprites.Count; i++)
        {
            boxes[i].GetComponent<Image>().sprite = sprites[i];
            switch (sprites[i].name)
            {
                case "Alcohol Bottle":
                    doorDialogue.HaveAlcohol = true;
                    break;
                case "TrapSchematics":
                    snakeTrapScript.HaveSchematics  = true;
                    break;
                case "GiftBox":
                    snakeTrapScript.HaveBox = true;
                    print("cogido box "+snakeTrapScript.HaveBox);
                    break;
            }
        }
    }

    IEnumerator ObjectShow(GameObject toShow)
    {
        for(int i=0; i < toShow.transform.childCount; i++)
        {
            toShow.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(waitingSecs);
        }
    }
    
    public List<Sprite> Sprites
    {
        get
        {
            return sprites;
        }

        set
        {
            sprites = value;
        }
    }

    public List<GameObject> Boxes
    {
        get
        {
            return boxes;
        }

        set
        {
            boxes = value;
        }
    }
}

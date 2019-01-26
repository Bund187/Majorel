using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Gamepad : MonoBehaviour {

    public GameObject[] inventorySets;
    public GameObject title, targetScript;

    protected List<GameObject> array = new List<GameObject>();
    protected int index = 0;

    private void Awake()
    {
        for (int l = 0; l < inventorySets.Length; l++)
        {
            for (int j = 0; j < inventorySets[l].transform.childCount; j++)
            {
                for (int k = 0; k < inventorySets[l].transform.GetChild(j).transform.childCount; k++)
                {
                    array.Add(inventorySets[l].transform.GetChild(j).transform.GetChild(k).gameObject);
                }
            }
        }
    }

    public abstract void ExitInventory();

    public abstract void NavigationDown();

    public abstract void NavigationUp();

    public abstract void SelectedObject();

    public abstract void SlotChanger(bool add, List<GameObject> objHighlight);

    public void ColorChanger(Color newColor)
    {
        ColorBlock color = array[index].GetComponent<Button>().colors;
        color.normalColor = newColor;
        array[index].GetComponent<Button>().colors = color;
    }

    public void SelectedSubMenu(Color changeColor)
    {
        Color color = title.GetComponent<Image>().color;
        color = changeColor;
        title.GetComponent<Image>().color = color;
        ColorChanger(Color.yellow);
    }

    public List<GameObject> Array
    {
        get
        {
            return array;
        }

        set
        {
            array = value;
        }
    }
}

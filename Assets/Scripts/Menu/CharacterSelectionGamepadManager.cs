using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterSelectionGamepadManager : Gamepad {

    public override void ExitInventory()
    {
        SelectedSubMenu(Color.white);
        ColorChanger(Color.white);
    }

    public override void NavigationDown()
    {
        ColorChanger(Color.white);
        if ((index >= 0 && index <= 17))
            index += 9;

        ColorChanger(Color.yellow);
    }

    public override void NavigationUp()
    {
        ColorChanger(Color.white);
        if ((index >= 9 && index <= 26))
            index -= 9;

        ColorChanger(Color.yellow);
    }

    public override void SelectedObject()
    {
        targetScript.GetComponent<CharacterSelectionBehaviour>().CharacterSelect(index.ToString());
        targetScript.GetComponent<CharacterSelectionBehaviour>().SkinPicker(Array[index]);
    }
    
    public override void SlotChanger(bool add, List<GameObject> objHighlight)
    {
        ColorChanger(Color.white);
        if (add)
            index++;
        else
            index--;

        if (index >= objHighlight.Count)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = objHighlight.Count - 1;
        }
        ColorChanger(Color.yellow);

    }
}

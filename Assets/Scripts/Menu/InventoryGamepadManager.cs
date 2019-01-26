using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryGamepadManager : Gamepad {

    public GameObject inventoryButton, gamepadMenu;

    GamepadMenuController GamepadController;
    
    void Start()
    {
        GamepadController = gamepadMenu.GetComponent<GamepadMenuController>();
    }

   void Update () {
        if(title.GetComponent<Image>().color == Color.red)
            GamepadController.PressXButton();
    }

    public override void ExitInventory()
    {
        SelectedSubMenu(Color.white);
        ColorChanger(Color.white);
        targetScript.GetComponent<InventoryTextBoxBehaviour>().ObjectCollected(title);
    }

    public override void NavigationDown() 
    {
        ColorChanger(Color.white);
        if ((index >= 0 && index <= 3) || (index >= 8 && index <= 11))
            index += 4;
        ColorChanger(Color.yellow);
    }

    public override void NavigationUp()
    {
        ColorChanger(Color.white);
        if ((index >= 4 && index <= 7) || (index >= 12 && index <= 15))
            index -= 4;
        ColorChanger(Color.yellow);
    }

    // FUNCION QUE CONTROLA AL PULSAR LA X DONDE CAMBIAMOS DE BLOQUE DE INVENTARIO
    public void InventorySwap()
    {
        inventoryButton.GetComponent<InventoryArrowBehaviour>().ButtonPressed();
        ColorChanger(Color.white);
        index += 8;
        if(index>= array.Count)
        {
            index = 0;
        }
        ColorChanger(Color.yellow);
    }

    public override void SelectedObject()
    {
        targetScript.GetComponent<InventoryTextBoxBehaviour>().ObjectCollected(array[index]);
    }
    
    public override void SlotChanger(bool add, List<GameObject> objHighlight)
    {
        ColorChanger(Color.white);
        if (add)
        {
            index++;
            if (index%8==0)
                inventoryButton.GetComponent<InventoryArrowBehaviour>().ButtonPressed();
        }
        else
        {
            index--;
            if (index%7==0)
                inventoryButton.GetComponent<InventoryArrowBehaviour>().ButtonPressed();
        }

        if (index < 0 || index >= objHighlight.Count)
            inventoryButton.GetComponent<InventoryArrowBehaviour>().ButtonPressed();

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

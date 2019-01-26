using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamepadMenuController : MonoBehaviour {

    public GameObject[] highlights, characters;
    public GameObject inventoryTextBox, goInventory, goCharSelection,goNotes;

    float r1button, l1button, vertical, horizontal;
    bool isR1, isL1,isVerticalDown,isVerticalUp, isSubMenu;
    int highlightIndex=0;

    InventoryGamepadManager Inventory;
    CharacterSelectionGamepadManager CharacterSelection;
    NotesGamepadManager Notes;

    void Start () {
        highlights[0].SetActive(true);
        Inventory = goInventory.GetComponent<InventoryGamepadManager>();
        CharacterSelection = goCharSelection.GetComponent<CharacterSelectionGamepadManager>();
        Notes = goNotes.GetComponent<NotesGamepadManager>();
    }

    void Update () {
        GamepadNavigationRight();
        GamepadNavigationLeft();
        GamepadNavigationUp();
        GamepadNavigationDown();
        SelectSubMenu();
        SelectObject();
        Exit();
    }

    public void Exit()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButton("Menu"))
        {
            if (highlightIndex == 0)
                Inventory.ExitInventory();
            else if (highlightIndex == 1)
                CharacterSelection.ExitInventory();
            else if (highlightIndex == 2)
                Notes.DeactivateNotes();
            isSubMenu = false;
        }
    }

    public void SelectObject()
    {
        vertical = Input.GetAxis("CrossVertical");
        horizontal = Input.GetAxis("CrossHorizontal");
        if (Input.anyKey || vertical!=0 || horizontal!=0)
        {
            if (highlightIndex == 0)
                Inventory.SelectedObject();
            else if (highlightIndex == 1)
                CharacterSelection.SelectedObject();
        }
    }
    public void SelectSubMenu()
    {
        if (Input.GetButtonDown("Submit"))
        {
            switch (highlightIndex)
            {
                case 0:
                    isSubMenu = true;
                    Inventory.SelectedSubMenu(Color.red);
                    break;
                case 1:
                    isSubMenu = true;
                    CharacterSelection.SelectedSubMenu(Color.red);
                    break;
                case 2:
                    isSubMenu = true;
                    Notes.SelectedSubMenu(Color.red);
                    Notes.ActivateNotes();
                    break;
            }
        }
    }

    public void PressXButton()
    {
        if (highlightIndex == 0)
        {
            if (Input.GetButtonDown("Block"))
            {
                Inventory.InventorySwap();
            }
        }
    }

    public void GamepadNavigationRight()
    {
        r1button = Input.GetAxisRaw("R1");
        horizontal = Input.GetAxis("CrossHorizontal");
        if (r1button != 0 || horizontal>0)
        {
            if (!isR1)
            {
                isR1 = true;
                if (!isSubMenu)
                    HiglightChanger(true, highlights);
                else
                {
                    if (highlightIndex == 0)
                    {
                        Inventory.SlotChanger(true, Inventory.Array);
                    }
                    if (highlightIndex == 1)
                    {
                        CharacterSelection.SlotChanger(true, CharacterSelection.Array);
                    }
                }
            }

        }
        else
        {
            isR1 = false;
        }
    }
    public void GamepadNavigationLeft()
    {
        l1button = Input.GetAxisRaw("L1");
        horizontal = Input.GetAxis("CrossHorizontal");
        if (l1button != 0 || horizontal < 0)
        {
            if (!isL1)
            {
                isL1 = true;
                if (!isSubMenu)
                    HiglightChanger(false, highlights);
                else {
                    if (highlightIndex == 0)
                    {
                        Inventory.SlotChanger(false, Inventory.Array);
                    }
                    if (highlightIndex == 1)
                    {
                        CharacterSelection.SlotChanger(false, CharacterSelection.Array);
                    }
                }
        }

        }
        else
        {
            isL1 = false;
        }
    }

    public void GamepadNavigationUp()
    {
        vertical = Input.GetAxis("CrossVertical");
        if (vertical > 0)
        {
            if (!isVerticalUp)
            {
                isVerticalUp = true;
                if (isSubMenu)
                {
                    if (highlightIndex == 0)
                    {
                        Inventory.NavigationUp();
                    }
                    if (highlightIndex == 1)
                    {
                        print("navegacion arriba");
                        CharacterSelection.NavigationUp();
                    }
                }
            }

        }
        else
            isVerticalUp = false;
    }

    public void GamepadNavigationDown()
    {
        vertical = Input.GetAxis("CrossVertical");
        if (vertical < 0 )
        {
            if (!isVerticalDown)
            {
                isVerticalDown = true;

                if (isSubMenu)
                {
                    if (highlightIndex == 0)
                    {
                         Inventory.NavigationDown();
                    }
                    if (highlightIndex == 1)
                    {
                        print("navegacion abajo");
                        CharacterSelection.NavigationDown();
                    }
                }
            }

        }
        else
            isVerticalDown = false;
    }

    public void HiglightChanger(bool add, GameObject[] objHighlight )
    {
        
        if (add)
            highlightIndex++;
        else
            highlightIndex--;

        if (highlightIndex >= objHighlight.Length)
        {
            highlightIndex = 0;
        }
        if (highlightIndex < 0)
        {
            highlightIndex = objHighlight.Length-1;
        }
        for (int i=0; i< objHighlight.Length; i++)
        {
            if (i == highlightIndex)
            {
                objHighlight[i].SetActive(true);
            }
            else
            {
                objHighlight[i].SetActive(false);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NotesGamepadManager : MonoBehaviour {


    public GameObject gamepadMenu, title, bigNotes;

    GamepadMenuController GamepadController;

    void Start () {
        GamepadController = gamepadMenu.GetComponent<GamepadMenuController>();
    }
	    
    public void SelectedSubMenu(Color changeColor)
    {
        Color color = title.GetComponent<Image>().color;
        color = changeColor;
        title.GetComponent<Image>().color = color;
    }
    
    public void ActivateNotes()
    {
        bigNotes.SetActive(true);
    }

    public void DeactivateNotes()
    {
        bigNotes.GetComponent<PaperNoteBehaviour>().FadeOut();
    }

}

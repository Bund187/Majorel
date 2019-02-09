using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToogleStatueButton : MonoBehaviour {

    public Sprite pressed;
    public StatueSuccessManager successScript;

    private Sprite nonPressed;

    public void SwapSprite()
    {
        nonPressed = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = pressed;
        pressed = nonPressed;

        if (gameObject.name=="ButtonDeath1" || gameObject.name== "ButtonJoker2" || gameObject.name == "ButtonGolem3" || gameObject.name == "ButtonBeggar4" || gameObject.name == "ButtonCreature5")
        {
            if (GetComponent<Image>().sprite.name.Contains("Push"))
            {
                successScript.Success++;
                print("suma succes number " + successScript.Success);
                successScript.CheckSuccess();
            }
            else
            {
                successScript.Success--;
                print("resta succes number " + successScript.Success);
            }
        }
        else
        {
            successScript.LockSuccess = !successScript.LockSuccess;
            print("lockSuccess " + successScript.LockSuccess);
        }
    }
}

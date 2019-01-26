using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NavigationDialogueLines : MonoBehaviour {

    public Button[] dialogueLineButtons;
    public Dialogue_UIManager dialogueScript;

    private int lineNumber;
    private bool isVertical, isAction;

   /* private void OnEnable()
    {
        lineNumber = 0;
        dialogueLineButtons[lineNumber].Select();
    }*/

    void Update () {
        MoveThroughLines();
    }

    void MoveThroughLines()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        int auxLine;

        auxLine = lineNumber;

        if (vertical > 0)
        {
            if (!isVertical)
            {
                isVertical = true;
                lineNumber--;
            }
        }
        else if (vertical < 0)
        {
            if (!isVertical)
            {
                isVertical = true;
                lineNumber++;
            }
        }
        else
        {
            isVertical = false;
        }

        CheckMinMax();
        while (!dialogueLineButtons[lineNumber].gameObject.activeSelf)
        {
            if (lineNumber > auxLine)
                lineNumber++;
            else if (lineNumber < auxLine)
                lineNumber--;
        }

        CheckMinMax();
        dialogueLineButtons[lineNumber].Select();
        SubmitButton();
        //ResetColor();
    }

    void SubmitButton()
    {
        if (Input.GetAxisRaw("Submit") != 0)
        {
            if (!isAction)
            {
                //ChangeColor();
                isAction = true;
                dialogueLineButtons[lineNumber].onClick.AddListener(() => TaskOnClick());
            }
        }
        else
        {
            isAction = false;
        }
    }

    void TaskOnClick()
    {
        print("Linea Select" + lineNumber);
        dialogueScript.PlayerChoice(lineNumber);
    }

    void CheckMinMax()
    {

        if (lineNumber >= dialogueLineButtons.Length)
            lineNumber = 0;
        if (lineNumber < 0)
            lineNumber = dialogueLineButtons.Length - 1;
    }

    /*void ChangeColor()
    {
        ColorBlock highColor = dialogueLineButtons[lineNumber].colors;
        highColor.pressedColor = new Color(0.5f,0.0327f,1);
        dialogueLineButtons[lineNumber].colors = highColor;
        
    }*/

    /*void ResetColor()
    {
        for(int i = 0; i < dialogueLineButtons.Length; i++)
        {
            if(i!= lineNumber)
            {
                dialogueLineButtons[lineNumber].Select();
            }
        }
    }*/
}

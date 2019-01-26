using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour {

    public GameObject canvas, canvasMenu, canvasDialogue;
    public PlayerController playerScript;

    bool exitOn;

    void Update () {
        Activate();
    }

    void Activate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitOn = !exitOn;

            if (Camera.main.orthographicSize < 5 && !exitOn)
            {
                print("canvas off");
                canvasDialogue.SetActive(true);
            }
        }
        if (exitOn)
        {
            playerScript.IsMenuOn = false;
            canvasMenu.SetActive(playerScript.IsMenuOn);
            if (Camera.main.orthographicSize < 5)
            {
                print("canvas on");
                canvasDialogue.SetActive(false);
            }

            Time.timeScale = 0;
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
            }
        }
        else
        {
            canvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

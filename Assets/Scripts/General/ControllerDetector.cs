using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControllerDetector : MonoBehaviour {

    //float horizontal, vertical, action, block, menu, cancel;
    float timer;
    private string[] controller;
    int i = 0;

    public RuntimeAnimatorController inventoryBtn_Xbox, inventoryBtn_PC;
    public GameObject goInventoryBtn, joystickEnable, notesBack;
    public Sprite notesBackButton, notesBackArrow;
    public GameObject[] meoanBtns;

    private void Start()
    {
        timer = 1;
    }
    
    void Update () {
       
        ControllerDetection();
    }
    
    public void ControllerDetection()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            controller = Input.GetJoystickNames();

            if (controller.Length > 0)
            {
                while(i<controller.Length)
                {
                    if (!string.IsNullOrEmpty(controller[i]))
                    {
//                        print("mando conectado =" + controller[i]);
                        goInventoryBtn.GetComponent<Animator>().runtimeAnimatorController = inventoryBtn_Xbox;
                        joystickEnable.SetActive(true);
                        ActivateMeoanButtons(true);
                        notesBack.GetComponent<Image>().sprite = notesBackButton;
                        i = controller.Length;
                    }
                    if (i == controller.Length - 1)
                    {
                        joystickEnable.SetActive(false);
                        print("mando desconectado");
                        ActivateMeoanButtons(false);
                        goInventoryBtn.GetComponent<Animator>().runtimeAnimatorController = inventoryBtn_PC;
                        notesBack.GetComponent<Image>().sprite = notesBackArrow;
                    }
                    i++;
                }
                timer = 1;
                i = 0;
            }
        }
    }

    void ActivateMeoanButtons(bool activate)
    {
        for(int i=0; i< meoanBtns.Length; i++)
        {
            meoanBtns[i].SetActive(activate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour {

    private bool canAdd;
    float rotationX;

    public GameObject clock;
    TimeManager TimeManager;

    private void Awake()
    {
        TimeManager = clock.GetComponent<TimeManager>();
    }


    private void Start()
    {
        canAdd=true;
    }

    void Update () {
        DayCicle();
    }

    public void DayCicle()
    {
        if (TimeManager.Hours >= 05 && TimeManager.Hours <= 07)
        {
            if (TimeManager.RealSeconds % 5 == 0)
            {
                if (canAdd)
                {
                    rotationX = transform.localEulerAngles.x - 1.25f;
                    transform.localEulerAngles = new Vector3((rotationX), 0, 0);
                    canAdd = false;
                }
            }
            else
            {
                canAdd = true;
            }
        }

        if (TimeManager.Hours >= 08 && TimeManager.Hours <= 15)
        {
            rotationX = 0;
            transform.rotation = Quaternion.Euler(rotationX, 0, 0);
        }

        if (TimeManager.Hours >= 16 && TimeManager.Hours <= 18)
        {
            if (TimeManager.RealSeconds % 5 == 0)
            {
                if (canAdd)
                {
                    rotationX = transform.localEulerAngles.x + 1.25f;
                    transform.localEulerAngles = new Vector3((rotationX), 0, 0);
                    canAdd = false;
                }
            }
            else
            {
                canAdd = true;
            }
        }
        if (TimeManager.Hours >= 19 && TimeManager.Hours <= 23)
        {
            rotationX = 90;
            transform.rotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}

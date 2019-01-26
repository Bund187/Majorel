using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour {

    public Sprite[] shadows;
    public GameObject clock;

    TimeManager TimeManager;
    SpriteRenderer spriteR;
    int i;
    bool canAdd, canStart;

    private void Awake()
    {
        TimeManager = clock.GetComponent<TimeManager>();
        spriteR = GetComponent<SpriteRenderer>();
        i = 0;
        canAdd = true;
    }

    // Update is called once per frame
    void Update () {
        DayCicle();
    }

    public void DayCicle()
    {
        spriteR.sprite = shadows[i];

        if (TimeManager.Hours == 06 && TimeManager.Minutes == 40)
        {
            canStart = true;
            
        }
        if (TimeManager.Hours == 20 && TimeManager.Minutes == 00)
        {
            canStart = true;
            i = 0;

        }
        if ((TimeManager.Hours == 6 || TimeManager.Hours == 7 || TimeManager.Hours == 18 || TimeManager.Hours==19) && canStart)
        {
            if (TimeManager.Minutes % 5 == 0)
            {
                if (canAdd)
                {
                    i++;
                    canAdd = false;
                }
            }
            else
            {
                canAdd = true;
            }
        }
        
        
    }
}

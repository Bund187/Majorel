using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public int interval;
    public float TimeMachine;
    public int hours, minutes;
    public Animator anim;
    public GameObject demo;

    private Text textTime;
    private float timer;
    private bool canAdd;
    private int realSeconds;

    void Start()
    {
        textTime = transform.gameObject.GetComponent<Text>();
        hours = 04;
        minutes = 57;
        canAdd = true;
    }

    void Update()
    {
        GameTime();
        ChangeUIClock();
        Demo();
    }

    void Demo()
    {
        if (hours == 00)
        {
            demo.SetActive(true);
        }
    }
    public void GameTime()
    {
        textTime.text = string.Format("{0:00}:{1:00}", hours, minutes);
        //Time.timeScale =TimeMachine;

        timer += Time.deltaTime;
        int realMinutes = Mathf.FloorToInt(timer / 60F);
        realSeconds = Mathf.FloorToInt(timer - realMinutes * 60);
       // print("Minutes "+ Mathf.FloorToInt(timer) + " Seconds "+ Mathf.FloorToInt(timer - realMinutes * 60));
        if (realSeconds % interval == 0)
        {
            if (canAdd)
            {
                minutes++;
                if (minutes == 60)
                {
                    minutes = 00;
                    hours++;
                    if (hours == 24) hours = 00;
                }
                canAdd = false;
            }
        }else
        {
            canAdd = true;
        }
        
    }

    void ChangeUIClock()
    {
        if (hours == 6 && minutes == 00)
        {
            anim.enabled = true;
            print("dawn");
            anim.SetTrigger("dawn");
        }
        if (hours == 19 && minutes == 00)
        {
            print("dusk");
            anim.SetTrigger("dusk");
        }
    }


    public int Hours
    {
        get
        {
            return hours;
        }

        set
        {
            hours = value;
        }
    }

    public int Minutes
    {
        get
        {
            return minutes;
        }

        set
        {
            minutes = value;
        }
    }

    public int RealSeconds
    {
        get
        {
            return realSeconds;
        }

        set
        {
            realSeconds = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehaviour{

    float time;

    public float Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }
    }

    public bool Wait(float waitTime)
    {
        bool trigger=false;
        if (UnityEngine.Time.time > Time + waitTime)
        {
            trigger = true;
        }

        return trigger;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueIconBehaviour : MonoBehaviour {

    float temp;
    float waitingTime;

    private void Awake()
    {
        waitingTime = 1.7f;
    }
    void Update () {
        Wait();
    }

    void Wait()
    {
        if (Time.time > temp + waitingTime)
        {
            this.gameObject.SetActive(false);
            
        }
    }
    public float Temp
    {
        get
        {
            return temp;
        }

        set
        {
            temp = value;
        }
    }
}

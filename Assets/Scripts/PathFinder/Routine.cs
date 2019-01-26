using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routine : MonoBehaviour{

    public GameObject[] targets;
    public TimeManager clock;
    public int hours, minutes;

    private void Awake()
    {
        if (gameObject.name != "Date Rosita")
        {
            for (int i = 0; i < targets.Length - 1; i++)
            {
                targets[i] = transform.GetChild(i).gameObject;
            }
        }
    }

    public Routine()
    {
    }
    public Routine(GameObject[] _targets, TimeManager _clock, int _hours, int _minutes)
    {
        GameObject[] targets = _targets;
        TimeManager clock = _clock;
        int hours = _hours;
        int minutes = _minutes;
    }
}

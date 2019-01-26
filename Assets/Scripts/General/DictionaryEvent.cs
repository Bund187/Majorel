using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryEvent : MonoBehaviour{

    Dictionary<string, bool> events = new Dictionary<string, bool>();

    private void Awake()
    {
        events.Add("drunk", false);
        events.Add("pidgeon", false);
        events.Add("address", false);
        events.Add("flee", false);
        events.Add("dated", false);
        events.Add("trap", false);
    }

    public Dictionary<string, bool> Events
    {
        get
        {
            return events;
        }

        set
        {
            events = value;
        }
    }
}

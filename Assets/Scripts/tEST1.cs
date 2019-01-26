using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tEST1 : MonoBehaviour {

    public DictionaryEvent dictionaryEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dictionaryEvent.Events["trap"] = true;
    }
}

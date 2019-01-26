using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour {

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.anyKey)
        {
            print("any key");
            anim.SetTrigger("cycle");
        }	
	}
}

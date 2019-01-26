using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLeavesTrigger : MonoBehaviour {

    public int limit;

    Animator anim;
    bool isFound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update () {
        if (Mathf.FloorToInt(Time.time) % 5 == 0)
        {
            isFound = false;
            PlayRandomLeaves();
        }
    }

    void PlayRandomLeaves()
    {
        int rnd = Random.Range(0, limit);
        if (rnd == 1 && !isFound)
        {
            anim.Play(0);
            isFound = true;
        }
    }
}

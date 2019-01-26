using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAnimation : MonoBehaviour {

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("move");
    }
}

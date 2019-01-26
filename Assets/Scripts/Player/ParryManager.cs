using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryManager : MonoBehaviour {

    private Animator anim;
    
    void Awake()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            collision.transform.parent.gameObject.GetComponent<PlayerStats>().Stats1.Health -= transform.parent.GetComponent<PlayerStats>().Stats1.Strength * 3;
            anim.SetTrigger("ParrySuccess");
            //collision.transform.parent.gameObject.GetComponent<EnemyCombatAI>().StartCoroutine("Freezer");
        }
    }
}

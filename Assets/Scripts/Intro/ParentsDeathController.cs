using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsDeathController : MonoBehaviour {

    Vector2 targetPosition;
    Animator anim;
    SpriteRenderer spriteR;
    float time;
    float hold;

	// Use this for initialization
	void Start () {
        targetPosition = new Vector2(-41f, -11.5f);
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        time = Time.time + 2;
    }
	
	// Update is called once per frame
	void Update () {
        Wait();
    }

    void Move()
    {
        anim.SetBool("isMoving", true);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime*2);
        if ((Vector2)transform.position == targetPosition)
        {
            spriteR.flipX = false;
            anim.SetBool("isMoving", false);
            anim.SetBool("cry", true);
        }
    }

    void Wait()
    {
        anim.SetBool("isMoving", false);
        if (Time.time > time + 0.5)
        {
            anim.SetBool("still", true);
            Move();
        }
    }
}

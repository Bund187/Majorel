using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPathfinding : MonoBehaviour {

    public GameObject[] targets;
    public GameObject sleep;
    public float speed;
    public TimeManager clock;
    public int hours,minutes;

    private int index,min;
    private float seconds;
    private bool isColliding, waitMinutes;
    private Animator anim;
    private SpriteRenderer sRend;
    
	void Awake () {
        index = 0;
        seconds = 0;
        anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        min = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (clock.Hours == hours)
            sleep.SetActive(false);
            PathFind();

        if (seconds > 0)
        {
            Wait();
        }
	}

    public void PathFind()
    {
        if (index < targets.Length-1 && !isColliding)
        {
            anim.SetBool("isMoving", true);
            if (targets[index].transform.position.x > transform.position.x)
            {
                sRend.flipX = false;
            }
            else
            {
                sRend.flipX = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, targets[index].transform.position, speed);

            if (transform.position == targets[index].transform.position)
            {
                print("location num" + index);
                LocationReached();
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void LocationReached()
    {
        
        switch (index)
        {
            case 0:
                sleep.SetActive(false);
                index++;
                break;
            case 4:
                PerformAtlocation(1);
                break;
            case 6:
                PerformAtlocation(1);
                break;
            case 9:
                anim.SetBool("isMoving", false);
                sleep.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            default:
                waitMinutes = false;
                index++;
                break;
        }
        
    }
    private void PerformAtlocation(int minutesQty)
    {
        anim.SetBool("isMoving", false);
        if (!waitMinutes)
        {
            min = clock.Minutes + minutesQty;
            if (min > 60)
            {
                min -= clock.Minutes;
            }
            waitMinutes = true;
        }
        NextLocation(clock.Hours, min);
    }
    private void NextLocation(int hour,int minute)
    {
        if (clock.Hours == hours && clock.Minutes == minute)
        {
            anim.SetBool("isMoving", true);
            index++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            seconds = Time.time+1f;
        }
    }

    public void Wait()
    {
        if (Time.time >= seconds)
        {
            isColliding = false;
            seconds = 0;
        }
    }
    
}

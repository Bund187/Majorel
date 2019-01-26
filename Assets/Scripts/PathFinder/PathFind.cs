using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour {

    public Routine[] routines;
    public Vector2 resetPosition;
    public TimeManager clock;
    public int hours, minutes;
    public float speed;

    protected int index;
    protected bool waitMinutes, reverse, startRoutine;
    protected float seconds;
    protected int routineIndex;
    protected Animator anim;

    private int min;
    private bool isColliding;
    private SpriteRenderer sRend;

    public bool IsColliding
    {
        get
        {
            return isColliding;
        }

        set
        {
            isColliding = value;
        }
    }

    private void Start()
    {
        index = 0;
        seconds = 0;
        anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
    }
    
    protected virtual void ResetRoutine()
    {
       // print("Reseteamos las rutinas");
        startRoutine = false;
        index = 0;
        reverse = false;
        transform.position = resetPosition;
        CalculateDailyRoutine();
    }

    protected virtual void CalculateDailyRoutine()
    {
        routineIndex = 0;//Random.Range(0, routines.Length);
    }

    public void PathFinder(Routine[] routineToFollow)
    {
        //print("Rutina en curso: "+ routineIndex +" " + routineToFollow[routineIndex]+ " "+ transform.gameObject.name +" Hora actual: " + clock.hours+":"+clock.minutes+" Hora Rutina: "+ routineToFollow[routineIndex].hours+":"+ routineToFollow[routineIndex].minutes);
        if (index < routineToFollow[routineIndex].targets.Length - 1 && !isColliding)
        {
            anim.SetBool("isMoving", true);
            if(reverse && index < 0)
            {
                index = 0;
                startRoutine = false;
                print("Acaba la rutina donde empezo");
            }
            if (routineToFollow[routineIndex].targets[index].transform.position.x > transform.position.x)
            {
                sRend.flipX = false;
            }
            else
            {
                sRend.flipX = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, routineToFollow[routineIndex].targets[index].transform.position, speed);
            if (transform.position == routineToFollow[routineIndex].targets[index].transform.position)
            {
                LocationReached();
            }
            else
            {
                LocationLeft();
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    protected virtual void LocationReached()
    {

    }
    protected virtual void LocationLeft()
    {
        
    }
    protected virtual void PerformAtlocation(int minutesQty, bool reverse)
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
        NextLocation(clock.Hours, min,reverse);
    }

    protected virtual void NextLocation(int hour, int minute, bool reverse)
    {
        //print("Hour="+hour);
        anim.SetBool("isMoving", false);
        if (((clock.Hours == hour && clock.Minutes >= minute)|| clock.Hours > hour) || clock.hours>=0 && clock.hours<=4)
        {
            anim.SetBool("isMoving", true);
            ManageIndex();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("NPC"))
        {
            seconds = Time.time + 1f;
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

    //SI SON LAS 00 O LA 1 REVERTIMOS LAS RUTINAS, PARA QUE VUELVAN A SUS CASAS
    protected void GoHome()
    {
        if (clock.hours == 0 || clock.hours == 1)
        {
            reverse = true;
        }
        else
        {
            reverse = false;
        }
    }

    protected void ManageIndex()
    {
        if (!reverse)
            index++;
        else
            index--;
    }
    
}

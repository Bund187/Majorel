using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuincarnonBlockDate : MonoBehaviour {

    public SayLineText sayLineScript;
    public LoadXml_Misc loadXmlScript;
    public GameObject deathCollider;

    WaitBehaviour waitScript = new WaitBehaviour();
    QuincarnonPathFind quincarnonScript;
    bool hasColided;
    int waitTime;

    private void Start()
    {
        waitTime = 5;
    }

    private void Update()
    {
        GetAway(false);
    }

    public void GetAway(bool flee)
    {
        if ((waitScript.Wait(waitTime) && hasColided)|| flee)
        {
            if (flee)
            {
                quincarnonScript.speed = 0.1f;
                SayLine(loadXmlScript.MiscClass.quincarnonFlee);
            }
            else
            {
                SayLine(loadXmlScript.MiscClass.quincarnonNoDate);
            }
            quincarnonScript.Index = 0;
            quincarnonScript.RoutineIndex = 1;
            quincarnonScript.SpecialRoutine = true;
            quincarnonScript.enabled = true;
            hasColided = false;
            deathCollider.SetActive(false);
        }
    }

    public void SayLine(string line)
    {
        sayLineScript.Talk(line);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Quincarnon")
        {
            deathCollider.SetActive(true);
            collision.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
            quincarnonScript = collision.gameObject.GetComponent<QuincarnonPathFind>();
            quincarnonScript.enabled = false;
            waitScript.Time = Time.time;
            hasColided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Quincarnon")
        {
            deathCollider.SetActive(false);
        }
    }

    public int WaitTime
    {
        get
        {
            return waitTime;
        }

        set
        {
            waitTime = value;
        }
    }
}

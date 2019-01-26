using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour {

    public GameObject canvasFlash;

    private GameObject enemy;
    private bool isHitting, isBlocking, isParrying, coolingDown, isDefend, isMoving, isParry, isDamaged, isDead;
    private Animator anim;
    static WaitForSeconds wait;
    int avoidFirstPress;
    StatsManager.Stats Stats;
    SpriteRenderer spriteR;

    void Start () {
        avoidFirstPress = 0;
        anim = GetComponent<Animator>();
        wait = new WaitForSeconds(0.1f);
        Stats = GetComponent<PlayerStats>().Stats1;
        spriteR = GetComponent<SpriteRenderer>();
    }
    
    void Update () {
      
        if (!isParry && !isDamaged && Stats.Health>0)
        {
            CombatMove();
            CombatControls();
        }
    }
    
    //CONTROLS
    void CombatMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 moving = new Vector2(horizontal, 0);
        transform.Translate(moving * Stats.Speed);
        if (horizontal > 0)
        {
            isMoving = true;
            anim.SetBool("isMoving", isMoving);
            
        }
        else if (horizontal < 0)
        {
            isMoving = true;
            anim.SetBool("isMoving", isMoving);

        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }
    }

    void CombatControls()
    {
        //Attacking
        if (Input.GetAxisRaw("Action") != 0)
        {
            if (!isHitting)
            {
                isHitting = true;
                //StartCoroutine("Attack");
                Attacking();
            }
        }
        else
        {
            isHitting = false;
            
        }

        //Defending
        if (Input.GetAxisRaw("Block") != 0)
        {
            if (!isBlocking)
            {
                isBlocking = true;
                isDefend = true;
                anim.SetBool("Defend", isDefend);
                //StartCoroutine("Parry");
            }
        }
        else
        {
            isBlocking = false;
            isDefend = false;
            anim.SetBool("Defend", isDefend);
            
        }

        //Parry
        if (!isMoving)
        {
            
            if (Input.GetAxisRaw("Cancel") != 0)
            {
                if (avoidFirstPress > 0)
                {
                    if (!isParrying)
                    {
                        isParrying = true;
                        StartCoroutine("Parry");
                    }
                }
            }
            else
            {
                isParrying = false;
                avoidFirstPress = 1;
            }
        }


    }

    public void TakeDamage()
    {
        canvasFlash.GetComponentInChildren<FlashDamage>().NewColor = new Color(1, 0, 0, 1);
        canvasFlash.SetActive(true);
        anim.SetTrigger("Damage");
        StartCoroutine(Damage());
    }

    void RaycastDamage()
    {
        Vector2 rayDirection;
        if (spriteR.flipX)
            rayDirection = Vector2.left;
        else
            rayDirection = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1.5f, 1 << LayerMask.NameToLayer("Enemy"));
        Debug.DrawRay(transform.position, rayDirection * 1.5f, Color.red);
        if (hit.collider != null)
        {
            print("HIT! " + hit.collider.gameObject.name);
            GetComponent<AttackManager>().DoDamage(hit);
        }
    }

    void Attacking()
    {
        if (!coolingDown)
        {
            anim.SetTrigger("Fire");
            RaycastDamage();
            StartCoroutine(CoolDown(new WaitForSeconds(0.5f)));
        }
    }

    IEnumerator Damage()
    {
        isDamaged = true;
        yield return wait;
        isDamaged = false;
    }

    IEnumerator Parry()
    {
        if (!coolingDown)
        {
            anim.SetTrigger("Parry");
            print("Oh yes Parry");
            transform.GetChild(1).gameObject.SetActive(true);
            yield return wait;
            transform.GetChild(1).gameObject.SetActive(false);
            //AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            StartCoroutine(CoolDown(new WaitForSeconds(1)));
        }
    }

    //IEnumerator Attack()
    //{
    //    if (!coolingDown)
    //    {
    //        anim.SetTrigger("Fire");
    //        transform.GetChild(0).gameObject.SetActive(true);
    //        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
    //        //print("Animacion=" + m_CurrentClipInfo[0].clip.name +" lenght "+ m_CurrentClipInfo[0].clip.length);
    //        yield return wait;// new WaitForSeconds(m_CurrentClipInfo[0].clip.length);
    //        transform.GetChild(0).gameObject.SetActive(false);
    //        StartCoroutine(CoolDown(new WaitForSeconds(0.7f)));
    //    }
    //}

    IEnumerator CoolDown(WaitForSeconds coolWait)
    {
        coolingDown = true;
        yield return coolWait;
        coolingDown = false;
    }
    
    public GameObject Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }

    public bool IsDefend
    {
        get
        {
            return isDefend;
        }

        set
        {
            isDefend = value;
        }
    }
}

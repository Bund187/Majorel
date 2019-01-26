using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject combatManager, menu,transmutation, startCombatTitle;
   
    Rigidbody2D rb;
    Animator anim;
    bool isMoving, isAttacking, isMenuOn, isMenuPress, isTalking;
    SpriteRenderer sRender;
    string oldname;
    PlayerCombatController PlayerCombatController;
    CombatManager CombatManager;

   
    void Start () {
        if(menu!=null)
            menu.SetActive(isMenuOn);
        rb =GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRender = GetComponent<SpriteRenderer>();
        oldname = GetComponent<PlayerStats>().Stats1.Named;
        PlayerCombatController = GetComponent<PlayerCombatController>();
        if(combatManager!=null)
            CombatManager = combatManager.GetComponent<CombatManager>();
    }
	
	void FixedUpdate () {
        if (!isAttacking && !isMenuOn && !isTalking)
        {
            Move();
        }
    }

    private void Update()
    {
        if (!isAttacking && !isTalking)
            OpenMenu();
    }

    void OpenMenu()
    {
        if (Input.GetAxisRaw("Menu") != 0)
        {
            if (!isMenuPress)
            {
                isMenuPress = true;
                ToggleMenu();
                if (GetComponent<PlayerStats>().Stats1.Named != oldname)
                {
                    transmutation.SetActive(true);
                }
                oldname = GetComponent<PlayerStats>().Stats1.Named;
            }
        }
        else
        {
            isMenuPress = false;
            
        }
    }

    public void ToggleMenu()
    {
        isMenuOn = !isMenuOn;
        menu.SetActive(isMenuOn);
    }

    void Move()
    {

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

       
        if (horizontal>0)
        {
            isMoving = true;
            sRender.flipX = false;
        }
        else if (horizontal < 0)
        {
            isMoving = true;
            sRender.flipX = true;
        }
        else if(vertical > 0 || vertical < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        anim.SetBool("isMoving", isMoving);
        Vector2 moving = new Vector2(horizontal, vertical);
        transform.Translate(moving *speed);
    }

    //ACTIVATE COMBAT
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (!isAttacking && !isMenuOn && !IsTalking)
    //    {
    //        if (collision.gameObject.tag.Equals("NPC"))
    //        {
    //            if (Input.GetAxisRaw("Cancel") != 0)
    //            {
    //                if (!isAction)
    //                {
    //                    isAction = true;
    //                    if (transform.position.x < collision.transform.position.x)
    //                    {
    //                        CombatManager.IsPlayerAtLeft = true;
    //                        sRender.flipX = false;
    //                    }
    //                    else
    //                    {
    //                        CombatManager.IsPlayerAtLeft = false;
    //                        sRender.flipX = true;
    //                    }
    //                    CombatManager.Enemy = collision.gameObject;
    //                    CombatManager.CombatSetUp();
    //                    startCombatTitle.SetActive(true);
    //                }
    //            }
    //            else
    //            {
    //                isAction = false;
    //            }
    //        }
    //    }
    //}
    
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }

    public bool IsTalking
    {
        get
        {
            return isTalking;
        }

        set
        {
            isTalking = value;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }

    public bool IsMenuOn
    {
        get
        {
            return isMenuOn;
        }

        set
        {
            isMenuOn = value;
        }
    }

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }

        set
        {
            isMoving = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatAI : MonoBehaviour {

    public GameObject canvasFlash;

    float holdAttackTimer;
    GameObject player;
    bool isEnemyAtRight, isTouching, inAction,isInitialPositionOff, /*coolingDown,*/ isDefend, isFreeze,canAttack;
    int randomNumber;
    Animator anim;
    float time;
    StatsManager StatsManager;
    StatsManager.Stats Stats;


    static WaitForSeconds waitAttack,waitDefend;

    private int highValue, mediumHighValue, mediumValue, lowValue;

    private void Awake()
    {
        canAttack = true;
        StatsManager = GetComponent<PlayerStats>();
        if (StatsManager.aggressive)
        {
            highValue = 80;
            mediumHighValue = 85;
            mediumValue = 95;
            lowValue = 97;
        }
        if (StatsManager.balanced)
        {
            print("balanceado");
            highValue = 40;
            mediumHighValue = 60;
            mediumValue = 75;
            lowValue = 90;
        }
        if (StatsManager.passive)
        {
            print("pasivo");
            highValue = 30;
            mediumHighValue = 50;
            mediumValue = 70;
            lowValue = 90;
        }

    }

    void Start()
    {
        Stats = GetComponent<PlayerStats>().Stats1;
        anim = GetComponent<Animator>();
        waitAttack = new WaitForSeconds(0.2f);
        waitDefend = new WaitForSeconds(5);
        if (GetComponent<SpriteRenderer>().flipX)
            isEnemyAtRight = true;

    }
	
	void Update () {

        if (Stats.Health > 0 /*&& !IsFreeze*/)
        {
            Eyes();
        }
        else
            print(gameObject.name + "Se ha muerto");
        //if (isFreeze) {
        //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
        //    {
        //        isFreeze = false;
        //    }
        //}
        
    }

    private void FixedUpdate()
    {
        //DebugRay();
    }
    public void Eyes()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        if (horizontal > 0)
        {
            //el player se esta acercando
            DecisionCloser();
        }
        else if (horizontal < 0)
        {
            //el player se esta alejando
            DecisionAway();
        }
        else
        {
            //Player parado
            DecisionStand();
        }
        if (Input.GetAxisRaw("Action") != 0) {
            //Player Atacando
            DecisionAttack();
        }
        if (Input.GetAxisRaw("Block") != 0)
        {
            //Player Defendiendo
            DecisionDefend();
        }
        
    }


//DECISIONS
    public void DecisionAttack()
    {
        if (!inAction) Random.Range(1, 100);
        if (randomNumber <= /*50*/highValue)
        {
            //ATTACK
            Attack();
            //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
        }
        else if (randomNumber > /*50*/highValue && randomNumber <= /*55*/mediumHighValue)
        {
            //DEFEND
            Defend();
        }
        else if (randomNumber > mediumHighValue && randomNumber <= /*65*/mediumValue)
        {
            //BACKWARD
            BackwardPreparation();
        }
        else
        {
            //WAIT
            WaitPreparation();
        }
    }

    public void DecisionDefend()
    {
        if (!inAction) Random.Range(1, 100);
        if (randomNumber <= highValue)
        {
            //ATTACK
            Attack();
        }
        else if (randomNumber > highValue && randomNumber <= mediumHighValue)
        {
            //DEFEND
            Defend();
        }
        else if (randomNumber > mediumHighValue && randomNumber <= mediumValue)
        {
            //BACKWARD
            BackwardPreparation();
        }
        else if (randomNumber > mediumValue && randomNumber <= lowValue)
        {
            //FORWARD
            ForwardPreparation();
        }
        else
        {
            //WAIT
            WaitPreparation();
        }
    }

    public void DecisionCloser()
    {
        if (!inAction) Random.Range(1, 100);
        if (randomNumber <= highValue)
        {
            //ATTACK
            Attack();
        }
        else if (randomNumber > highValue && randomNumber <= mediumHighValue)
        {
            //FORWARD
            ForwardPreparation();
        }
        else if (randomNumber > mediumHighValue && randomNumber <= mediumValue)
        {
            //BACKWARD
            BackwardPreparation();
        }
        else if (randomNumber > mediumValue && randomNumber <= lowValue)
        {
            //WAIT
            WaitPreparation();
        }
        else
        {
            //DEFEND
            Defend();
        }
    }

    public void DecisionAway()
    {
        if (!inAction) Random.Range(1, 100);
        if (randomNumber <= highValue)
        {
            //FORWARD
            ForwardPreparation();
        }
        else if (randomNumber > highValue && randomNumber <= mediumHighValue)
        {
            //WAIT
            WaitPreparation();
        }
        else if (randomNumber > mediumHighValue && randomNumber <= mediumValue)
        {
            //ATTACK
            Attack();
        }
        else if (randomNumber > mediumValue && randomNumber <= lowValue)
        {
            //BACKWARD
            BackwardPreparation();
        }
        else
        {
            //DEFEND
            Defend();
        }
    }

    public void DecisionStand()
    {
        if (!inAction) randomNumber = Random.Range(1, 100);
        if (randomNumber <= highValue)
        {
            //FORWARD
            ForwardPreparation();

        }
        else if (randomNumber > highValue && randomNumber <= mediumHighValue)
        {
            //BACKWARD
            BackwardPreparation();
        }
        else
        {
            //WAIT
            WaitPreparation();
        }
    }

//PREPARATION FOR THE ACTIONS
    public void WaitPreparation()
    {
        anim.SetBool("isMoving", false);
        if (!inAction) time = Time.realtimeSinceStartup;
        Wait(time);
    }

    public void BackwardPreparation()
    {
        isDefend = false;
        anim.SetBool("Defend", isDefend);
        Backward(transform.position);
    }

    public void ForwardPreparation()
    {
        anim.SetBool("Defend", false);
        Forward(transform.position);
    }

//ACTIONS
    void Forward(Vector2 currentPos)
    {
        
        anim.SetBool("isMoving", true);
        float rndNum = Random.Range(0.5f, 1.0f);
        
        if(!isTouching)
        {
            inAction = true;
            if(isEnemyAtRight)
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(currentPos.x - rndNum, player.transform.position.y), Stats.Speed);
            else
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(currentPos.x + rndNum, player.transform.position.y), Stats.Speed);

            int rndStop= Random.Range(1, 50);
            if (isTouching || rndStop==1)
                inAction = false;
        }
        else
        {
            inAction = false;
        }
    }

    void Backward(Vector2 currentPos)
    {
        anim.SetBool("isMoving", true);
       
        //AnimatorClipInfo[]  m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //anim.animation.
        //print("Animacion=" + m_CurrentClipInfo[0].clip.name +" lenght "+ m_CurrentClipInfo[0].clip.length);
        
        //anim.SetBool("isMovingBack", true);
        float rndNum = Random.Range(0.1f, 0.5f);
       
        inAction = true;
        if(isEnemyAtRight)
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(currentPos.x + rndNum, player.transform.position.y), Stats.Speed);
        else
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(currentPos.x - rndNum, player.transform.position.y), Stats.Speed);

        int rndStop = Random.Range(1, 50);
        if (rndStop == 1)
            inAction = false;
        
    }

    void Wait(float waitTime)
    {
        float rndNum = Random.Range(0.5f,2.0f);
        inAction = true;
        if (Time.realtimeSinceStartup>= waitTime + rndNum)
        {
            inAction = false;
        }
    }

    void Attack()
    {
        if (Time.time >= holdAttackTimer)
        {
            canAttack = true;
        }
        if (canAttack)
        {
            anim.SetTrigger("Fire");
            StartCoroutine("ActivateForParry");
            RaycastDamage();
            holdAttackTimer = Time.time + 1;
            canAttack = false;
        }
        
    }

    IEnumerator ActivateForParry()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return /*waitAttack*/ new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        
    }

    void RaycastDamage()
    {
        Vector2 rayDirection;
        if (isEnemyAtRight)
            rayDirection = Vector2.left;
        else
            rayDirection = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1.5f, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, rayDirection * 1.5f, Color.red);
        if (hit.collider != null)
        {
            //print("HIT! " + hit.collider.gameObject.name);
            GetComponent<AttackManager>().DoDamage(hit);
        }
    }

   public void TakeDamage()
    {
        anim.SetTrigger("Damage");
        canvasFlash.GetComponentInChildren<FlashDamage>().NewColor = new Color(1, 1, 1, 1);
        canvasFlash.SetActive(true);
    }

    //public IEnumerator Freezer()
    //{
    //    isFreeze = true;
    //    Time.timeScale = 0.1f;
    //    TakeDamage();
    //    float pauseEndTime = Time.realtimeSinceStartup + 0.5f;
    //    while (Time.realtimeSinceStartup < pauseEndTime)
    //    {
    //        yield return 0;
    //    }
    //    Time.timeScale = 1;
    //    isFreeze = false;
    //}


    //IEnumerator Freeze()
    //{
        
    //    isFreeze = true;
    //    yield return new WaitForSeconds(0.5f);
        
    //    isFreeze = false;
    //}

    public void Defend()
    {
        isDefend = true;
        anim.SetBool("Defend", isDefend);
        if (!inAction) time = Time.realtimeSinceStartup;
        Wait(time);
    }

// When touching the player
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isInitialPositionOff)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                isTouching = true;
                if (!inAction) randomNumber = Random.Range(1, 100);

                if (randomNumber <= 60)
                {
                    Attack();
                }
                else if (randomNumber > 60 && randomNumber < 90)
                {
                    Defend();
                }
                else
                {
                    BackwardPreparation();
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isInitialPositionOff)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                isDefend = false;
                anim.SetBool("Defend", isDefend);
                isTouching = false;
            }
        }
    }
    
//GETTERS & SETTERS
    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public bool IsInitialPositionOff
    {
        get
        {
            return isInitialPositionOff;
        }

        set
        {
            isInitialPositionOff = value;
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

    //public bool IsFreeze
    //{
    //    get
    //    {
    //        return isFreeze;
    //    }

    //    set
    //    {
    //        isFreeze = value;
    //    }
    //}
}

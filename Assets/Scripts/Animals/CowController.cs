using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour {

    Animator anim;
    bool isWalking, upOrDown, flip;
    SpriteRenderer spriteR;
    
	void Awake () {
        anim = GetComponent<Animator>();
        isWalking = true;
        spriteR = GetComponent<SpriteRenderer>();
        flip = true;
        
    }
	
	void Update () {
        Action();
        if (upOrDown)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                isWalking = !isWalking;
                upOrDown = false;
                flip = true;
            }
        }
	}
    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("walk"))
        {
            if (spriteR.flipX)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
        }
        if (isWalking && !upOrDown)
        {
            if (flip)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
                {
                    int rndMove = Random.Range(0, 2);
                    if (rndMove == 0)
                    {
                        spriteR.flipX = true;

                    }
                    else
                    {
                        spriteR.flipX = false;

                    }
                    flip = false;
                }
            }
        }
    }

    void Action()
    {
        if (isWalking && !upOrDown)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                
                int rnd = Random.Range(0, 200);
                if (rnd < 10)
                {
                    anim.SetTrigger("down");
                    upOrDown = true;
                }
            }
        }
        else if(!isWalking && !upOrDown)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                int rnd = Random.Range(0, 200);
                if (rnd < 2)
                {
                    anim.SetTrigger("up");
                    upOrDown = true;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAnimGO : MonoBehaviour {

    Animator anim;
    PlayerController playerScript;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        playerScript.IsTalking = true;
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.815f && !anim.IsInTransition(0))
        {
            gameObject.SetActive(false);
            playerScript.IsTalking = false;
        }
    }
}

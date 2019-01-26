using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombatUI : MonoBehaviour {

    public GameObject combatManager;

    CombatManager CombatManager;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        CombatManager = combatManager.GetComponent<CombatManager>();

    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
        {
            //  CombatManager.CombatSetUp();
            CombatManager.CombatSetUpFinal();
            gameObject.SetActive(false);
        }

    }
}

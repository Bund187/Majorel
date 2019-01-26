using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour {

    public GameObject combatManager;

    SpriteRenderer spriteR;
    float colorValue, lessColor;
    CombatManager CombatManager;

	// Use this for initialization
	void Start () {
        spriteR = GetComponent<SpriteRenderer>();
        colorValue = 1;
        CombatManager = combatManager.GetComponent<CombatManager>();
        lessColor = 0.001f;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<PlayerStats>().Stats1.Health <= 0)
        {
            FadeOut();
        }
	}

    void FadeOut()
    {
        if (colorValue > 0)
        {
            spriteR.color = new Color(colorValue-= lessColor, colorValue -= lessColor, colorValue -= lessColor, colorValue -= lessColor);
            
        }
        else
        {
            CombatManager.CombatFinished();
        }
    }
}

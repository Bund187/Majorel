using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour {

    public GameObject player;
    public float speed;
    public Vector3 combatDistance;

    private Vector3 startPosition;
    private bool positionIsReached;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!positionIsReached)
            StartCombat();
    }

    void StartCombat()
    {
        Vector3 posicionDeseadaDerecha = startPosition + combatDistance;
        Vector3 posicionDeseadaIzquierda = startPosition - combatDistance;

        //if (transform.position.x >= posicionDeseadaDerecha.x - 0.1 || transform.position.x <= posicionDeseadaIzquierda.x + 0.1f)
        //{
            //Se inicializa la AI
            GetComponent<EnemyCombatAI>().enabled = true;
            positionIsReached=true;
            GetComponent<EnemyCombatAI>().IsInitialPositionOff = true;
        //}
        //else
        //{
        //    if (player.transform.position.x - transform.position.x <= 0)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, posicionDeseadaDerecha, speed);
        //    }
        //    else
        //    {
        //        transform.position = Vector3.Lerp(transform.position, posicionDeseadaIzquierda, speed);
        //    }
        //}
        
    }
}

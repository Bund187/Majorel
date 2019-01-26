using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPanelController : MonoBehaviour {

    public bool left, right, up, down;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if(left)
            transform.Translate(Vector3.left*(Time.deltaTime*speed));
        if(up)
            transform.Translate(Vector3.up * (Time.deltaTime*speed));
    }
}

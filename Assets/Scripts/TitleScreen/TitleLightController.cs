using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLightController : MonoBehaviour {

    RectTransform rectT;
    bool moveRight;
    public float speed;

	// Use this for initialization
	void Start () {
        rectT = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        if (rectT.localPosition.x >= 700)
            moveRight = false;   
        if(rectT.localPosition.x <= -700)
            moveRight = true;

        if (moveRight)
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        else
            transform.Translate(Vector2.left * Time.deltaTime * speed);
    }
}

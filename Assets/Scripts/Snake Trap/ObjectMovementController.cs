using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectMovementController : MonoBehaviour {

    public Button[] trapObjs; 

    bool mousePressed;

    void Update () {
        if (mousePressed)
        {
            DragObj();
        }
        else
        {
            
        }
	}

    private void DragObj()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 thisPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = thisPosition;
    }

    public void DeactivateOther()
    {
        for (int i = 0; i < trapObjs.Length; i++)
        {
            if (transform.name != trapObjs[i].gameObject.name)
            {
                trapObjs[i].enabled = false;
            }
        }
    }

    public void MouseDrag()
    {
        mousePressed = !mousePressed;
        if (mousePressed)
        {
            DeactivateOther();
            if(this.name== "RopeSegmentKnot")
            {
                RigidbodyConstraints2D constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Rigidbody2D>().constraints = constraints;
            }
        }
        else
        {
            for (int i = 0; i < trapObjs.Length; i++)
            {
                trapObjs[i].enabled = true;
            }
            if (this.name == "RopeSegmentKnot")
            {
                RigidbodyConstraints2D constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().constraints = constraints;
            }
        }
    }

    public bool MousePressed
    {
        get
        {
            return mousePressed;
        }

        set
        {
            mousePressed = value;
        }
    }

}

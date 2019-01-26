using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlaceController : MonoBehaviour {

    public GameObject[] correctObject;
    public SnakeTrapController snakeTrapScript;
    public int correctPlace;

    protected bool mousePress;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            mousePress = collision.gameObject.GetComponent<ObjectMovementController>().MousePressed;
            if (mousePress == false)
            {
                for (int i = 0; i < correctObject.Length; i++)
                {
                    if (collision.gameObject == correctObject[i])
                    {
                        PlaceRightposition(collision.gameObject);
                        snakeTrapScript.CheckTrap();
                    }
                }
            }
        }
    }

    protected virtual void PlaceRightposition(GameObject go)
    {
        
    }
    
}

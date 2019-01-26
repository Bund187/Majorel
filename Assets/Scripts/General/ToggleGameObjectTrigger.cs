using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectTrigger : MonoBehaviour {

    public bool activate;
    public GameObject toggleGameObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("colision entra");
            if (activate)
            {
                toggleGameObject.SetActive(true);
            }
            else
            {
                toggleGameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("colision sale");
            if (activate)
            {
                toggleGameObject.SetActive(false);
            }
            else
            {
                toggleGameObject.SetActive(true);
            }
        }
    }
}

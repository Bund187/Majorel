using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour {

    public GameObject goToDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        goToDeactivate.SetActive(false);
        print("Deactivate object " + false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goToDeactivate.SetActive(true);
        print("Deactivate object " + true);
    }
}

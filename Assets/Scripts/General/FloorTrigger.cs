using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour {

    public GameObject floor2, floor1, polygons2nd, polygonCollider1st, baranda;
    bool activate,deactivate;

    private void Start()
    {
        deactivate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            
            activate = !activate;
            deactivate = !deactivate;
            print("entra el player " + activate);
            floor2.SetActive(activate);
            polygons2nd.SetActive(activate);
            floor1.SetActive(deactivate);
            polygonCollider1st.SetActive(deactivate);
            baranda.SetActive(deactivate);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.transform.position.x < transform.position.x && !deactivate)
            {
                activate = !activate;
                deactivate = !deactivate;

                floor2.SetActive(activate);
                polygons2nd.SetActive(activate);
                floor1.SetActive(deactivate);
                polygonCollider1st.SetActive(deactivate);
                baranda.SetActive(deactivate);
            }
        }

    }
}

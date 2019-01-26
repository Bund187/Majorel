using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRightPlace : RightPlaceController
{
    protected override void PlaceRightposition(GameObject go)
    {
      
        go.transform.position = transform.position;

        correctPlace = 1;
        print(gameObject.name + " " + correctPlace);
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == "trap")
        {
            if (correctPlace == 1)
            {
                correctPlace = 0;
                print(gameObject.name + " " + correctPlace);
            }

        }
    }
}

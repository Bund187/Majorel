using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpHookStickPlacer : RightPlaceController
{
    public float x, y;
    public UpHookStickPlacer twinScript;

    private bool twinPlace;

    protected override void PlaceRightposition(GameObject go)
    {
        
        twinPlace = true;
        if (twinScript.TwinPlace)
        {

            correctPlace = 1;
            print(gameObject.name + " " + correctPlace);
        }
        
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

    public bool TwinPlace
    {
        get
        {
            return twinPlace;
        }

        set
        {
            twinPlace = value;
        }
    }
}

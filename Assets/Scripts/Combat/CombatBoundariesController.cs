using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBoundariesController : MonoBehaviour {

    public GameObject boundRight,boundLeft;

    public void CalculeBoundary(Camera camara, float originalOrthographicsSize, bool relocateBounds)
    {
        transform.position = new Vector2(camara.transform.position.x, camara.transform.position.y);
        float dividedBy = Screen.height / (camara.orthographicSize * 2);
        float scaleX = Screen.width / dividedBy;
        transform.localScale = new Vector2(scaleX, Screen.height / (originalOrthographicsSize * 2));
        PlaceBounds(scaleX, relocateBounds);
    }

    private void PlaceBounds(float scaleX, bool relocateBounds)
    {
        if (relocateBounds)
        {
            boundRight.transform.localPosition = new Vector2(0,0);
            float xRightPosition = boundRight.transform.position.x + (scaleX / 2f);
            boundRight.transform.position = new Vector2(xRightPosition, boundRight.transform.position.y);

            boundLeft.transform.localPosition = new Vector2(0, 0);
            float xLeftPosition = boundLeft.transform.position.x - (scaleX / 2f);
            boundLeft.transform.position = new Vector2(xLeftPosition, boundLeft.transform.position.y);

            relocateBounds = false;
        }
    }

}

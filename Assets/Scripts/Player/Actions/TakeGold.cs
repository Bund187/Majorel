using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeGold : MonoBehaviour {

    public GameObject goldText, goldImage;
    public Text goldAmount;

    bool isAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("colision " + collision.gameObject);

        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetAxisRaw("Action") != 0)
            {
                print("Action");
                isAction = true;
                goldText.SetActive(true);
                goldImage.SetActive(true);
                goldAmount.text = (int.Parse(goldAmount.text) + 4).ToString();
                Destroy(this);
            }
            else
            {
                isAction = false;
            }
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public Text playerText, goldAmount, goldAmountShop;
    public TextAppearanceManager textAppearanceManager;
    public LoadXml_Misc loadXml;
    public GameObject shopCanvas;
    public PlayerController playerC;

    private bool isAction,shopping;
    private bool isCancel;
    private bool waitTalk;

    private void Update()
    {
        Exit();

        if (!textAppearanceManager.enabled && waitTalk)
        {
            StartShopping();
            waitTalk = false;
        }

    }

    private void Exit()
    {
        if (Input.GetAxisRaw("Cancel") != 0 && shopCanvas.activeSelf)
        {
            if (!isCancel)
            {
                isCancel = true;
                shopCanvas.SetActive(false);
                playerC.IsTalking = false;
                shopping = false;
            }
        }
        else
            isCancel = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Input.GetAxisRaw("Action") != 0 && !playerC.IsMenuOn && !shopping)
            {
                if (!isAction)
                {
                    collision.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                    Talk();
                    playerC.IsTalking = true;
                    isAction = true;
                    waitTalk = true;
                }
            }
            else
                isAction = false;
            
        }
    }
    private void Talk()
    {
        textAppearanceManager.Text = playerText;
        textAppearanceManager.Sentence = loadXml.MiscClass.shopSentence.ToCharArray();
        textAppearanceManager.enabled = true;
        
    }

    void StartShopping()
    {
        goldAmountShop.text = goldAmount.text;
        shopping = true;
        shopCanvas.SetActive(true);
    }
}

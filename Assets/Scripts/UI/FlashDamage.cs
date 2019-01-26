using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlashDamage : MonoBehaviour {

    Image img;
    float alpha;
    Color newColor;

    void Start () {
        img = GetComponent<Image>();
        alpha = 1;
    }
	
	void Update () {
        FadeOut();
    }

    public void FadeOut()
    {
        if (alpha >= 0)
        {
            img.color = new Color(newColor.r, newColor.g, newColor.b, alpha -= 0.1f);
            if (alpha <= 0)
            {
                transform.parent.gameObject.SetActive(false);
                alpha = 1;
            }
            if (alpha <= 0.1)  
                alpha = 0;
        }
    }

    public Color NewColor
    {
        get
        {
            return newColor;
        }

        set
        {
            newColor = value;
        }
    }

}

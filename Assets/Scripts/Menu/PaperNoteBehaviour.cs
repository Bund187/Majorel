using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperNoteBehaviour : MonoBehaviour {

    public GameObject text;
    public Image backArrow;

    Image image;
    float alpha;
    bool isFadingOut;

    void Start () {
        image = GetComponent<Image>();
        alpha = 0;
    }
	
	void Update () {
        if (!isFadingOut)
            FadeIn();
        else
            FadeOut();
    }

    void FadeIn()
    {
        if (alpha <= 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha += 0.05f);
            backArrow.color = new Color(image.color.r, image.color.g, image.color.b, alpha += 0.05f);
        }
        else
        {
            text.SetActive(true);
        }
    }
   
    public void FadeOut()
    {
        isFadingOut = true;
        text.SetActive(false);
        if (alpha > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha -= 0.05f);
            backArrow.color = new Color(image.color.r, image.color.g, image.color.b, alpha -= 0.05f);
            if (alpha <= 0.1)
                alpha = 0;
        }
        else
        {
            isFadingOut = false;
            this.gameObject.SetActive(false);
        }
       
    }
      
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecentKilledController : MonoBehaviour {

    public LoadXml_Misc loadXmlScript;
    public float fadeSpeed;

    private string npcName;
    private TextMeshProUGUI textMesh;
    private bool isFading;
    private WaitBehaviour waitScript = new WaitBehaviour();

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (waitScript.Wait(2))
        {
            FadeIn();
        }
    }
    
    void FadeIn()
    {
        textMesh.text = npcName + "\n" + loadXmlScript.MiscClass.isDead;
        Color alpha = textMesh.color;
        if (!isFading)
        {
            alpha.a += fadeSpeed;
            textMesh.color = alpha;
        }
        else
            FadeOut();

        if (alpha.a >= 1)
        {
            isFading = true;
        }
    }

    public void FadeOut()
    {
        Color alpha = textMesh.color;
        alpha.a -= fadeSpeed;
        textMesh.color = alpha;

        if (alpha.a <= 0.05f)
        {
            alpha.a = 0;
            textMesh.color = alpha;
            this.enabled = false;
        }
    }

    public string NpcName
    {
        get
        {
            return npcName;
        }

        set
        {
            npcName = value;
        }
    }

    public bool IsFading
    {
        get
        {
            return isFading;
        }

        set
        {
            isFading = value;
        }
    }

    public WaitBehaviour WaitScript
    {
        get
        {
            return waitScript;
        }

        set
        {
            waitScript = value;
        }
    }
}

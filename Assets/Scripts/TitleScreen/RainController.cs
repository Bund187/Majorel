using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    public GameObject lightThunder, lightTitle, music,pressKey, menu;
    public ChangeCursor changeCursorScript;

    public AudioClip thunderAudio;
    public AudioSource audioS;
   
    Animator anim;
    bool isKey, canThunder;

    private void Start()
    {
        TextAsset texto = Resources.Load<TextAsset>("TestResources");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isKey)
        {
            if (Input.anyKey)
            {
               // changeCursorScript.showCursor = true;
                changeCursorScript.SetCursorVisible(true);
                lightThunder.SetActive(true);
                anim.SetTrigger("thunder");
                audioS.clip = thunderAudio;
                audioS.Play();
                StartCoroutine(WaitForSound(thunderAudio.length));
                isKey = true;
                pressKey.SetActive(false);
                menu.SetActive(true);
                
            }
        }
        else
        {
            Thunder();
        }

        if ((anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.815f && !anim.IsInTransition(0))&& isKey)
        {
            
            lightTitle.SetActive(true);
            music.SetActive(true);
        }
    }

    void Thunder()
    {
        int rnd = Random.Range(0, 1000);
        if ((rnd >= 5|| rnd<=10) && canThunder)
        {
            print("Rnd " + Time.time);
            lightThunder.SetActive(true);
            anim.SetTrigger("thunder");
            audioS.clip = thunderAudio;
            audioS.Play();
            StartCoroutine(WaitForSound(thunderAudio.length));
        }
    }

    IEnumerator WaitForSound(float duration)
    {
        canThunder = false;
        yield return new WaitForSeconds(0.2f);
        lightThunder.SetActive(false);
        yield return new WaitForSeconds(duration);
        canThunder = true;
    }
}

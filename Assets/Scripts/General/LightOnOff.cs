using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour {

    public GameObject[] lights;
    public TimeManager timeManager;
	
	// Update is called once per frame
	void Update () {
        LightControl();
        print("luces " + gameObject.name);
    }

    void LightControl()
    {
        if (timeManager.Hours == 7)
        {
            print("son las 7");
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
        if (timeManager.Hours == 18)
        {
            print("son las 20");
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
        }
    }
}

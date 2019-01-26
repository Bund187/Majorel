using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoOver : MonoBehaviour {

	void Update () {
        Time.timeScale = 0;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
        }
	}
}

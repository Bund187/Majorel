using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMouseController : MonoBehaviour {

	void Update () {
		
	}

    void MouseControl()
    {
        float horizontal = Mathf.Abs(Input.GetAxis("Joystick X"));
        float vertical = Mathf.Abs(Input.GetAxis("Joystick Y"));
        
        Vector2 mousePos = new Vector2(horizontal, vertical);
    }
}

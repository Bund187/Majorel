using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationOff : MonoBehaviour {

	void Update () {
        if (GetComponent<SpriteRenderer>().sprite.name.Equals("Transmutation9"))
        {
            this.gameObject.SetActive(false);
        }
	}
}

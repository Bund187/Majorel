using UnityEngine;
using System.Collections;

public class Depthmanager : MonoBehaviour {


	SpriteRenderer spriteRend;
	int order;
	
	void Awake () {
		spriteRend = GetComponent<SpriteRenderer> ();
		order = spriteRend.sortingOrder;
    }

	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag.Equals("Player")) spriteRend.sortingOrder = order;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player")) spriteRend.sortingOrder = col.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }
	
}

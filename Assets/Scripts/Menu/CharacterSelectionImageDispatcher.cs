using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionImageDispatcher : MonoBehaviour {

    public Sprite[] characterSprites = new Sprite[27];
    public Image[] characters = new Image[27];


    public void SpriteAdder(int i)
    {
        characters[i].sprite = characterSprites[i];
    }
}

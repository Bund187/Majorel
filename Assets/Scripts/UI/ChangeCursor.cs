using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool isTown;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        SetCursorVisible(false);
    }

    public void SetCursorVisible(bool show)
    {
        if (!isTown)
            Cursor.visible = show;
        else
            Cursor.visible = true;
    }
   
}

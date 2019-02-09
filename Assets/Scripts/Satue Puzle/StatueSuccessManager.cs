using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueSuccessManager : MonoBehaviour
{

    private int success;
    private bool lockSuccess;

    private void Start()
    {
        lockSuccess = false;
    }

    public void CheckSuccess()
    {
        if (success >= 5 && !lockSuccess)
        {
            PuzzleSuccess();
        }
    }

    public void PuzzleSuccess()
    {
        print("Puzle solucionado!!");
    }

    public bool LockSuccess
    {
        get
        {
            return lockSuccess;
        }

        set
        {
            lockSuccess = value;
        }
    }

    public int Success
    {
        get
        {
            return success;
        }

        set
        {
            success = value;
        }
    }
}

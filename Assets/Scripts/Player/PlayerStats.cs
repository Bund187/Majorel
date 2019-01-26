using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StatsManager {

    private void Awake()
    {
        switch (gameObject.name)
        {
            case "Majorel":
                Stats1 = new Stats(0, 1, 10, 0.05f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.majorelDesc);
                break;
            case "Mathias":
                Stats1 = new Stats(1, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.mathiasDesc);
                break;
            case "Quincarnon":
                Stats1 = new Stats(2, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.quincarnonDesc);
                break;
            case "Kip":
                Stats1 = new Stats(9, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.kipDesc);
                break;
            case "Rosita":
                Stats1 = new Stats(15, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.rositaDesc);
                break;
            case "Marga":
                Stats1 = new Stats(4, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.margaDesc);
                break;
            case "Aldis":
                Stats1 = new Stats(11, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.aldisDesc);
                break;
            case "Royce":
                Stats1 = new Stats(10, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, xmlScript.MiscClass.royceDesc);
                break;
            case "MathiasKill":
                Stats1 = new Stats(1, 2, 5, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController, "");
                break;
        }

        Stats1.Strength *= Stats1.Level;
    }
}

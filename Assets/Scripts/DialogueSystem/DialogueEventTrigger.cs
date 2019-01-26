using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventTrigger : MonoBehaviour {

    public QuincarnonPathFind quincarnonPathScript;

    public void SentenceEventTrigger(string sentence, string character)
    {
        print("frase=" + sentence + " charcater=" + character);
        switch (character)
        {
            case "Quincarnon":
                if (sentence.Contains("30"))
                {
                    quincarnonPathScript.SetsDate();
                    print("Activamos el evento cita");
                }
                break;
            case "Kip":
                if (sentence.Contains("shop"))
                {
                    print("Activamos el nombre en la casa para el minimapa");
                }
                break;

        }
    }

}

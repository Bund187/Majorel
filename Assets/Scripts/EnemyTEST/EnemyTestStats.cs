using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestStats : StatsManager {

   
    private void Awake()
    {
        switch (gameObject.name)
        {
            case "Majorel": Stats1 = new Stats(0, 1, 10, 0.03f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController,"");
                break;
            case "Mathias Brew":
                Stats1 = new Stats(1, 2, 10, 0.02f, 100, gameObject.name, GetComponent<Animator>().runtimeAnimatorController,"");
                break;
        }
        
        Stats1.Strength *= Stats1.Level;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour {

    public GameObject majorel;

    StatsManager.Stats[] charStats = new StatsManager.Stats[27];

    private void Start()
    {
        for (int i = 0; i < charStats.Length; i++)
        {
            if (i == 0)
            {
                charStats[i] = majorel.GetComponent<PlayerStats>().Stats1;
            }
            else
            {
                charStats[i] = new StatsManager.Stats(0, 0, 0, 0, 0, "?", null,"");
            }
        }
    }


    public StatsManager.Stats[] CharStats
    {
        get
        {
            return charStats;
        }

        set
        {
            charStats = value;
        }
    }
}

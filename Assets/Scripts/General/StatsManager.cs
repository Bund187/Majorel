using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {

    public bool aggressive, balanced, passive;
    public LoadXml_Misc xmlScript;

    private float boundLeft, boundRight;
    Stats stats;

    public class Stats
    {
        
        int index, level, strength;
        float speed, health;
        string named;
        RuntimeAnimatorController anim;
        string description;

        public Stats(int i, int lvl, int str, float spd, float hlt, string nam, RuntimeAnimatorController anima, string desc)
        {
            index = i;
            level = lvl;
            strength = str;
            speed = spd;
            health = hlt;
            named = nam;
            Anim = anima;
            description = desc;
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public float Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public string Named
        {
            get
            {
                return named;
            }

            set
            {
                named = value;
            }
        }

        public RuntimeAnimatorController Anim
        {
            get
            {
                return anim;
            }

            set
            {
                anim = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }

    private void Update()
    {
        if (stats.Health <= 0)
        {
            NoHealth();
        }
    }

    protected virtual void NoHealth()
    {
        transform.GetComponent<Animator>().SetFloat("Health", 0);
        if(transform.GetComponent<EnemyCombatAI>()!=null)
            transform.GetComponent<EnemyCombatAI>().enabled = false;
        print(transform.gameObject.name + " se ha quedado sin vida");
    }

    public float BoundRight
    {
        get
        {
            return boundRight;
        }

        set
        {
            boundRight = value;
        }
    }

    public float BoundLeft
    {
        get
        {
            return boundLeft;
        }

        set
        {
            boundLeft = value;
        }
    }

    public Stats Stats1
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }
}

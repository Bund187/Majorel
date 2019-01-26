using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackManager : MonoBehaviour
{

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //EL PLAYER ATACA
    //    if (collision.gameObject.tag.Equals("NPC"))
    //    {
    //        //ENEMIGO SE DEFIENDE
    //        if (collision.gameObject.GetComponent<EnemyCombatAI>().IsDefend)
    //        {
    //            float defendDamage = (float)transform.parent.GetComponent<PlayerStats>().Stats1.Strength / (float)collision.gameObject.GetComponent<PlayerStats>().Stats1.Strength;
    //            collision.gameObject.GetComponent<PlayerStats>().Stats1.Health -= defendDamage;
    //        }

    //        //ENEMIGO NO DEFIENDE
    //        else
    //        {
    //            collision.gameObject.GetComponent<PlayerStats>().Stats1.Health -= transform.parent.GetComponent<PlayerStats>().Stats1.Strength;
    //            collision.gameObject.GetComponent<EnemyCombatAI>().TakeDamage();
    //        }
    //    }

    //    //EL ENEMIGO ATACA
    //    if (collision.gameObject.tag.Equals("Player"))
    //    {
    //        if (!collision.transform.GetChild(1).gameObject.activeSelf)
    //        {
    //            //PLAYER SE DEFIENDE
    //            if (collision.gameObject.GetComponent<PlayerCombatController>().IsDefend)
    //            {
    //                float defendDamage = (float)transform.parent.GetComponent<PlayerStats>().Stats1.Strength / (float)collision.gameObject.GetComponent<PlayerStats>().Stats1.Strength;
    //                collision.gameObject.GetComponent<PlayerStats>().Stats1.Health -= defendDamage;
    //            }

    //            //PLAYER NO DEFIENDE
    //            else
    //            {
    //                collision.gameObject.GetComponent<PlayerStats>().Stats1.Health -= transform.parent.GetComponent<PlayerStats>().Stats1.Strength;
    //                collision.gameObject.GetComponent<PlayerCombatController>().TakeDamage();
    //                print("Player dañado");
    //            }
    //        }
    //    }
    //}

    public void DoDamage(RaycastHit2D hit)
    {
        GameObject striker;
        EnemyCombatAI EnemyCombatAI;
        PlayerStats PlayerStats, EnemyStats;

        //EL PLAYER ATACA
        if (hit.collider.gameObject.tag.Equals("NPC"))
        {
            striker = hit.collider.gameObject;
            EnemyCombatAI = striker.GetComponent<EnemyCombatAI>();
            EnemyStats = hit.collider.gameObject.GetComponent<PlayerStats>();

            //ENEMIGO SE DEFIENDE
            if (EnemyCombatAI.IsDefend)
            {
                float defendDamage = (float)GetComponent<PlayerStats>().Stats1.Strength / (float)EnemyStats.Stats1.Strength;
                EnemyStats.Stats1.Health -= defendDamage;
            }

            //ENEMIGO NO DEFIENDE
            else
            {
                hit.collider.gameObject.GetComponent<PlayerStats>().Stats1.Health -= GetComponent<PlayerStats>().Stats1.Strength;
                hit.collider.gameObject.GetComponent<EnemyCombatAI>().TakeDamage();
            }
        }

        //EL ENEMIGO ATACA
        if (hit.collider.gameObject.tag.Equals("Player"))
        {
            striker = hit.collider.transform.parent.gameObject;
            PlayerStats = striker.GetComponent<PlayerStats>();

            if (!striker.transform.GetChild(1).gameObject.activeSelf)
            {
                //PLAYER SE DEFIENDE
                if (striker.GetComponent<PlayerCombatController>().IsDefend)
                {
                    float defendDamage = (float)GetComponent<PlayerStats>().Stats1.Strength / (float)PlayerStats.Stats1.Strength;
                    PlayerStats.Stats1.Health -= defendDamage;
                }

                //PLAYER NO DEFIENDE
                else
                {
                    PlayerStats.Stats1.Health -= GetComponent<PlayerStats>().Stats1.Strength;
                    striker.GetComponent<PlayerCombatController>().TakeDamage();
                       
                }
            }
        }
        
    }

}

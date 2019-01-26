using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour {

    int auxIndex = 0;
    Routine rositaRoutine;

    public Routine CalculateDistance(GameObject[] auxiliarPoints)
    {
        Routine dateRositaRoutine = new Routine();
        GameObject auxGo = DistanceCalculation(auxiliarPoints);
        GameObject nearestGo = null;

        //SI EL INDICE ES MENOR A 3 ENTONCES QUINCARNON SE ENCUENTRA CERCA DE LOS GAMEOBJECTS SUPERIORES POR LO QUE SE TIENE QUE CALCULAR CUAL DE LOS INFERIORES ESTÁ MÁS CERCA 
        if (auxIndex < 3)
        {
            int j = 3;
            float distance = 1000;
            float auxDistance = 1000;
            while (j < auxiliarPoints.Length)
            {
                auxDistance = Vector2.Distance(transform.position, auxiliarPoints[j].transform.position);
                if (distance > auxDistance)
                {
                    nearestGo = auxiliarPoints[j];
                    auxIndex = j;
                    distance = auxDistance;
                }
                j++;
            }
            dateRositaRoutine.targets = new GameObject[7];
            dateRositaRoutine.targets[0] = auxGo;
            dateRositaRoutine.targets[1] = nearestGo;
            for (int i = 0; i < rositaRoutine.targets.Length; i++)
            {
                dateRositaRoutine.targets[i + 2] = rositaRoutine.targets[i];
            }
        }
        else
        {
            dateRositaRoutine.targets = new GameObject[6];
            dateRositaRoutine.targets[0] = auxGo;
            for (int i = 0; i < rositaRoutine.targets.Length; i++)
            {
                dateRositaRoutine.targets[i + 1] = rositaRoutine.targets[i];
            }
        }
        
        return dateRositaRoutine;
    }
    
    //DADO UN ARRAY DE GAMEOBJECTS, CALCULA CUAL DE DICHOS GAMEOBJECTS SE ENCUENTRA MAS CERCANO AL TRANSFORM QUE LLEVA ESTE SCRIPT Y LO DEVUELVE
    public GameObject DistanceCalculation(GameObject[] arrayGos)
    {
        float distance = 1000, auxDistance = 1000;
        GameObject nearestGo = null;
        
        for (int i = 0; i < arrayGos.Length; i++)
        {
            auxDistance = Vector2.Distance(transform.position, arrayGos[i].transform.position);
            if (distance > auxDistance)
            {
                nearestGo = arrayGos[i];
                auxIndex = i;
                distance = auxDistance;
            }
        }
        return nearestGo;
    }

    public Routine RositaRoutine
    {
        get
        {
            return rositaRoutine;
        }

        set
        {
            rositaRoutine = value;
        }
    }
}

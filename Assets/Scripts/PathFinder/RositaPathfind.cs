using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RositaPathfind : PathFind
{

    public GameObject killCollider;

    void Update()
    {
        //A LAS 4:58 DE LA MAÑANA SE RESETEAN TODAS LAS RUTINAS DE LOS PERSONAJES
        if (clock.Hours == 4 && clock.Minutes == 58)
        {
            ResetRoutine();
        }

        if ((clock.Hours == routines[routineIndex].hours && clock.Hours >= routines[routineIndex].minutes) || clock.Hours > routines[routineIndex].hours)
            startRoutine = true;

        if (startRoutine)
            PathFinder(routines);

        if (seconds > 0)
        {
            Wait();
        }

        GoHome();
    }

    protected override void LocationReached()
    {
        switch (routineIndex)
        {
            //RUTINA 1
            case 0:
                switch (index)
                {
                    case 12:
                        NextLocation(13, 32, reverse);
                        break;
                    case 14:
                        NextLocation(16, 41, reverse);
                        startRoutine = false;
                        break;
                    case 16:
                        NextLocation(22, 41, reverse);
                        startRoutine = false;
                        break;
                    case 22:
                        print("Rosita termina rutina 1");
                        startRoutine = false;
                        break;
                    default:
                        waitMinutes = false;
                        ManageIndex();
                        break;
                }
                break;
            //RUTINA 2
            case 1:
                switch (index)
                {
                    case 9:
                        NextLocation(8, 30, reverse);
                        break;
                    case 15:
                        NextLocation(15, 15, reverse);
                        break;
                    case 19:
                        NextLocation(17, 40, reverse);
                        break;
                    default:
                        waitMinutes = false;
                        ManageIndex();
                        break;
                }
                break;
            //RUTINA 3
            case 2:
                switch (index)
                {
                    case 6:
                        NextLocation(10, 30, reverse);
                        break;

                    case 8:
                        NextLocation(11, 30, reverse);
                        break;
                    case 14:
                        NextLocation(13, 07, reverse);
                        break;
                    case 19:
                        NextLocation(16, 40, reverse);
                        break;
                    case 23:
                        NextLocation(19, 30, reverse);
                        break;
                    default:
                        waitMinutes = false;
                        ManageIndex();
                        break;
                }
                break;

        }
    }



    protected override void LocationLeft()
    {
        killCollider.SetActive(false);
    }
}

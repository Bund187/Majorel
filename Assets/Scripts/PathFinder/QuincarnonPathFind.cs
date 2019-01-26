using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuincarnonPathFind : PathFind {

    public GameObject killCollider;
    public Routine[] specialRoutines;
    public GameObject[] auxiliarPoints;
    public DictionaryEvent dictionaryScript;

    bool specialRoutine;
    int dateHour, dateMinutes;
    Routine dateRositaRoutine;
    DistanceCalculator distanceCalculatorScript;
    
    private void Awake()
    {
        distanceCalculatorScript = GetComponent<DistanceCalculator>();
        distanceCalculatorScript.RositaRoutine = specialRoutines[0];
    }

    void Update()
    {
        //A LAS 4:58 DE LA MAÑANA SE RESETEAN TODAS LAS RUTINAS DE LOS PERSONAJES
        if (clock.Hours == 4 && clock.Minutes == 58)
        {
            index = 0;
            specialRoutine = false;
            ResetRoutine();
            dictionaryScript.Events["dated"] = false;
        }

        //MIENTRAS NO SE HAYA CONCERTADO LA CITA HACEMOS LA RUTINA NORMAL
        if (!specialRoutine)
        {
            //DESDE LAS 22:30 HASTA LAS 4AM NO SE PUEDE CITAR A QUINCARNON
            if ((clock.hours >= 5 && clock.hours < 22))
            {
                dictionaryScript.Events["dated"] = false;
            }
            else
            {
                dictionaryScript.Events["dated"] = true;
            }
            //COMPROBAMOS QUE LA CITA SE HAYA CONCERTADO
            CheckStartdate();

            //SINO SE HA CONCERTADO Y ES LA HORA APROPIADA IRA HACIENDO LA RUTINA NORMAL
            if ((clock.Hours == routines[routineIndex].hours && clock.Hours >= routines[routineIndex].minutes) || clock.Hours > routines[routineIndex].hours)
                startRoutine = true;

            if (startRoutine)
                PathFinder(routines);
        }
        else //EN EL MOMENTO QUE LA CITA EMPIECE SE TERMINA LA ANTERIOR RUTINA
        {
            print("empieza la cita");
            dictionaryScript.Events["dated"] = true;
            CalculateDistance();
            if(startRoutine)
                PathFinder(specialRoutines);
        }

        GoHome();



        if (seconds > 0)
        {
            Wait();
        }
    }
    
    //CALCULA LA DISTANCIA CON 6 PUNTOS DADOS PARA QUE COJA UN CAMINO CORRECTO ANTES DE IR HACIA CASA DE ROSITA
    void CalculateDistance()
    {
        dateRositaRoutine = distanceCalculatorScript.CalculateDistance(auxiliarPoints);
        specialRoutines[0] = dateRositaRoutine;
    }

    //CHEKEA SI SE HA DE INICIAR LA CITA CON ROSITA
    void CheckStartdate()
    {
        if (dateHour != 0)
        {
            print("check start date");
            if (clock.hours == dateHour && clock.minutes >= dateMinutes)
            {
                index = 0;
                routineIndex = 0;
                specialRoutine = true;
            }
        }
    }

    //INICIALIZA LA CITA PARA 30 MINUTOS DESPUES DE QUE SE LE HAYA PREGUNTADO A QUINCARNON DE QUEDAR
    public void SetsDate()
    {
        if (clock.minutes >= 30)
        {
            dateMinutes = clock.minutes - 30;
            dateHour = clock.hours + 1;
        }
        else
        {
            dateHour = clock.hours;
            dateMinutes = clock.minutes+10;
        }
        print("dateHour=" + dateHour + " dateminutes=" + dateMinutes);
    }

    protected override void LocationReached()
    {
        //print("index " + index +" Routineindex "+ RoutineIndex);
        if (!specialRoutine)
        {
            switch (routineIndex) {
                //RUTINA 1
                case 0:
                    switch (index)
                    {
                        case 5:
                            NextLocation(23, 00,reverse);
                            break;
                        case 10 :
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
                        case 5:
                            NextLocation(12, 0, reverse);
                            break;

                        case 14:
                            NextLocation(23, 00, reverse);
                            break;
                        case 20:
                            print("Quincarnon termina rutina 2");
                            startRoutine = false;
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
                            NextLocation(12, 15, reverse);
                            break;
                        case 10:
                            NextLocation(13, 30, reverse);
                            break;
                        case 14:
                            NextLocation(23, 0, reverse);
                            break;
                        case 20:
                            print("Quincarnon termina rutina 3");
                            startRoutine = false;
                            break;
                        default:
                            waitMinutes = false;
                            ManageIndex();
                            break;
                    }
                    break;
            }
        }
        else
        {
            switch (routineIndex)
            {
                //RUTINA ESPECIAL 1 - QUEDAR CON ROSITA
                case 0:
                    switch (index)
                    {
                        default:
                            waitMinutes = false;
                            index++;
                            break;
                    }
                    break;
                //RUTINA ESPECIAL 2 - CITA CANCELADA
                case 1:
                    switch (index)
                    {
                        case (4):
                            NextLocation(23, 00, reverse);
                            break;
                        case (8):
                            print("fin de la rutina");
                            startRoutine = false;
                            specialRoutine = false;
                            break;
                        default:
                            waitMinutes = false;
                            index++;
                            break;
                    }
                    break;
            }
        }
        

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

    //CUANDO SE VA DE LA LOCALIZACIÓN DESACTIVA EL COLLIDER PARA MATARLO
    protected override void LocationLeft()
    {
        killCollider.SetActive(false);
    }


    public int RoutineIndex
    {
        get
        {
            return routineIndex;
        }

        set
        {
            routineIndex = value;
        }
    }

    public bool SpecialRoutine
    {
        get
        {
            return specialRoutine;
        }

        set
        {
            specialRoutine = value;
        }
    }
}

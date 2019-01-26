using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameObject player, cam, leftBound, rightBound, combatUI, fightCanvas;
    public Text clock;

    Vector2 playerInitialPosition, enemyInitialPosition;
    Vector3 cameraInitialPosition;
    GameObject enemy;
    float middlePoint, xPlayer, xEnemy;
    CameraCenterController CameraCenterController;
    CombatUIManager CombatUIManager;
    PlayerCombatController PlayerCombatController;
    bool canPlaceFighters, isPlayerAtLeft,combatReady;
    int fightersInPosition;

    private void Awake()
    {
        CameraCenterController = cam.GetComponent<CameraCenterController>();
        CombatUIManager = combatUI.GetComponent<CombatUIManager>();
        PlayerCombatController = player.GetComponent<PlayerCombatController>();
    }

    void Update()
    {
        if (canPlaceFighters)
        {
            FightersPosition(xPlayer, player);
            FightersPosition(xEnemy, enemy);
        }
        if (fightersInPosition >= 2) canPlaceFighters = false;
    }

    public void CombatSetUp()
    {
        //DESACTIVAMOS EL RELOJ DEL JUEGO
        clock.enabled = false;
        //ACTIVAMOS EL ESCENARIO DE LUCHA
        fightCanvas.SetActive(true);

        //TRASLADAMOS A LOS LUCHADORES y A LA CAMARA FUERA DEL MAPA PARA QUE NO HAYA COLLIDERS CERCA PERO PRIMERO ALMACENAMOS SU POSICION INICIAL
        playerInitialPosition = player.transform.position;
        WarpFighters(player);

        cameraInitialPosition = cam.transform.position;
        WarpFighters(cam);

        WarpFighters(enemy);

        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.transform.position.x != playerInitialPosition.x)
        {
            //HAYAMOS EL PUNTO MEDIO ENTRE OS DOS COMBATIENTES
            xPlayer = player.transform.position.x;
            xEnemy = enemy.transform.position.x;
            middlePoint = CameraCenterController.ScreenCenter(xPlayer, xEnemy);

            ////ACTIVAMOS EL ZOOM DE LA CAMARA, QUE IRÁ HACIA EL PUNTO MEDIO Y DESACTIVAMOS QUE LA CAMARA SIGA AL PLAYER
            CameraCenterController.FollowPlayer = false;
            CameraCenterController.ActivateZoom = true;

            ////FORZAMOS QUE LOS COMBATIENTES A SITUARSE A UNA DISTANCIA DEL PUNTO MEDIO CUANDO LO HAYAN HECHO CONTINUAMOS
            fightersInPosition = 0;
            canPlaceFighters = true;

            //IMPEDIMOS EL MOVIMIENTO NORMAL DEL PLAYER
            player.GetComponent<PlayerController>().IsAttacking = true;

            //SITUAMOS AL PLAYER Y AL ENEMIGO EN ANIMACION IDLE_ATTACK
            player.GetComponent<Animator>().SetBool("isAttack", true);
            enemy.GetComponent<Animator>().SetBool("isAttack", true);

            //COLOCAMOS AL ENEMIGO MIRANDO EN LA DIRECCION CORRECTA
            SpriteRenderer enemySR = enemy.GetComponent<SpriteRenderer>();

            if (isPlayerAtLeft)
                enemySR.flipX = true;
            else
                enemySR.flipX = false;
        }

    }

    public void CombatSetUpFinal()
    {
        //ACTIVAMOS EL COMBATTRIGGERCONTROLLER Y DESACTIVAMOS EL ACTIONTRIGGER Y EL PLAYERCONTROLLER
        player.transform.GetChild(3).gameObject.SetActive(true);
        player.transform.GetChild(2).gameObject.SetActive(false);
        player.GetComponent<PlayerController>().enabled = false;

        PlayerCombatController.Enemy = enemy;
        PlayerCombatController.enabled = true;

        //ACTIVAMOS LA IA Y ASIGNAMOS EL PLAYER A LA IA
        EnemyCombatAI enemyIA = enemy.GetComponent<EnemyCombatAI>();
        enemyIA.enabled = true;
        enemyIA.Player = player;
        enemyIA.IsInitialPositionOff = true;

        //ACTIVAMOS TODA LA UI DEL COMBATE
        CombatUIManager.Enemy = enemy;
        CombatUIManager.Player = player;
        CombatUIManager.enabled = true;
    }

    public void CombatFinished()
    {
        //DESACTIVAMOS TODA LA UI DEL COMBATE
        CombatUIManager.OnOffUI(false);
        CombatUIManager.enabled = false;

        //DESACTIVAMOS LA POSICION INICIAL DEL ENEMIGO Y DESTRUIMOS EL GAMEOBJECT DEL ENEMIGO
        EnemyCombatAI enemyIA = enemy.GetComponent<EnemyCombatAI>();
        enemyIA.IsInitialPositionOff = false;
        Destroy(enemy);

        //DESACTIVAMOS EL COMBATTRIGGERCONTROLLER Y ACTIVAMOS EL ACTIONTRIGGER Y EL PLAYERCONTROLLER
        player.transform.GetChild(3).gameObject.SetActive(false);
        player.transform.GetChild(2).gameObject.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        PlayerCombatController.enabled = false;

        //SITUAMOS AL PLAYER ANIMACION IDLE
        player.GetComponent<Animator>().SetBool("isAttack", false);

        //PERMITIMOS EL MOVIMIENTO NORMAL DEL PLAYER
        player.GetComponent<PlayerController>().IsAttacking = false;

        ////REINICIAMOS EL PUNTO MEDIO DE LOS JUGADORES
        fightersInPosition = 0;
        canPlaceFighters = false;

        //TRASLADAMOS A LOS LUCHADORES y A LA CAMARA AL MAPA
        player.transform.position = playerInitialPosition;
        cam.transform.position = cameraInitialPosition;

        ////ACTIVAMOS EL ZOOM OUT DE LA CAMARA Y QUE LA CAMARA SIGA AL PLAYER
        CameraCenterController.PlayerXPosition = player.transform.position.x;
        CameraCenterController.FollowPlayer = true;
        CameraCenterController.ActivateZoom = false;
    
        //ACTIVAMOS EL RELOJ DEL JUEGO
        clock.enabled = true;

        //DESACTIVAMOS EL ESCENARIO DE LUCHA
        fightCanvas.SetActive(false);
    }

    public void WarpFighters(GameObject fighter)
    {
        float newXPosition = fighter.transform.position.x + 1000;
        fighter.transform.position = new Vector2(newXPosition, fighter.transform.position.y);
    }

    public void FightersPosition(float xFighter, GameObject goFighter)
    {
        Vector2 newPosition;
        if (xFighter < middlePoint)
            newPosition = new Vector2(middlePoint - 2f, goFighter.transform.position.y);
        else
            newPosition = new Vector2(middlePoint + 2f, goFighter.transform.position.y);

        goFighter.transform.position = Vector2.MoveTowards(goFighter.transform.position,newPosition,Time.deltaTime*2);
        enemy.transform.position = new Vector2(enemy.transform.position.x, player.transform.position.y);

        if (goFighter.transform.position.x == newPosition.x && player.transform.position.y == enemy.transform.position.y)
            fightersInPosition += 1;

    }

    public GameObject Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }

    public bool IsPlayerAtLeft
    {
        get
        {
            return isPlayerAtLeft;
        }

        set
        {
            isPlayerAtLeft = value;
        }
    }

    public bool CombatReady
    {
        get
        {
            return combatReady;
        }

        set
        {
            combatReady = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeoanSetUp : MonoBehaviour {

    public GameObject board, rollDice, nextTurn, exitButton,shake, betSetup, startRoll, initialScore;
    public Text textGold, firstRollResult, actualGold, actualBet,score, opponentScore,pressAnyKey, bet;
    public GameObject[] dices, opponentDices;
    public DialogueSetupManager dialogueSetupScript;
    public SayLineText sayLine;
    public LoadXml_Misc loadXml;
    public Dialogue_UIManager uiManagerScript;

    MeoanController MeoanController;
    int gold, finalBet, rnd, otherRnd;
    bool waitKey, pressY, pressX, pressA;

    private void Awake()
    {
        MeoanController = GetComponent<MeoanController>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.M))
            SetUp();
        if (waitKey)
        {
            if (Input.anyKeyDown)
            {
                score.text = "0";
                opponentScore.text = "0";
                pressAnyKey.gameObject.SetActive(false);
                firstRollResult.gameObject.SetActive(false);
                EraseDices();
                MeoanController.enabled = true;
                MeoanController.IsTheEnd = false;
                if (rnd > otherRnd)
                {
                    rollDice.SetActive(true);
                    nextTurn.SetActive(true);
                    shake.SetActive(true);
                }
                else
                {
                    MeoanController.OpponentStarts = true;
                }
                this.enabled = false;
                waitKey = false;
            }
        }

        GamePadControl();

    }

    public void SetUp()
    {
        exitButton.SetActive(false);
        actualBet.text = "0";
        gold = int.Parse(textGold.text);
        uiManagerScript.MeoanIsOn = true;
        if (gold > 0)
        {
            board.SetActive(true);
            betSetup.SetActive(true);
            actualGold.text = textGold.text;
            finalBet = int.Parse(actualBet.text);
        }
        else
        {
            sayLine.Talk(loadXml.MiscClass.broke);
            Exit();
        }
    }

    void GamePadControl()
    {
        //Pulsar la Y
        if (Input.GetAxisRaw("Menu") != 0)
        {
            if (!pressY)
            {
                pressY = true;
                if (startRoll.activeSelf)
                    RollDiceStart();
                else
                    IncreaseBet();
            }
        }
        else
        {
            pressY = false;

        }

        //Pulsar la X
        if (Input.GetAxisRaw("Block") != 0)
        {
            if (!pressX)
            {
                pressX = true;
                DecreaseBet();
            }
        }
        else
        {
            pressX = false;

        }
        //Pulsar la A
        if (Input.GetAxisRaw("Submit") != 0)
        {
            if (!pressA)
            {
                pressA = true;
                StartMeoan();
            }
        }
        else
        {
            pressA = false;
        }
    }

    public void IncreaseBet()
    {
        pressAnyKey.gameObject.SetActive(false);
        if (finalBet < gold)
        {
            finalBet += 1;
            actualBet.text = finalBet.ToString();
        }
    }
    public void DecreaseBet()
    {
        pressAnyKey.gameObject.SetActive(false);
        if (finalBet > 0)
        {
            finalBet -= 1;
            actualBet.text = finalBet.ToString();
        }
    }

    public void StartMeoan()
    {
        if (finalBet <= 0)
        {
            pressAnyKey.text = "You MUST to bet some gold!!";
            pressAnyKey.gameObject.SetActive(true);
        }
        else
        {
            textGold.text = (int.Parse(textGold.text) - finalBet).ToString();
            betSetup.SetActive(false);
            startRoll.SetActive(true);
        }
    }

    public void RollDiceStart()
    {
        bet.text = (finalBet * 2).ToString();
        EraseDices();
        firstRollResult.gameObject.SetActive(false);
        pressAnyKey.gameObject.SetActive(false);
        rnd = Random.Range(0, 6);
        dices[rnd].SetActive(true);

        otherRnd = Random.Range(0, 6);
        opponentDices[otherRnd].SetActive(true);

        rnd++;
        score.text = rnd.ToString();
        otherRnd++;
        opponentScore.text = otherRnd.ToString();
         if (rnd > otherRnd)
        {
            RollingResult("Player starts");
        }
        else if (rnd < otherRnd)
        {
            RollingResult("Opponent starts");
        }
        else
        {
            firstRollResult.text = "Draw!";
            firstRollResult.gameObject.SetActive(true);
            pressAnyKey.text = "-Roll again-";
            pressAnyKey.gameObject.SetActive(true);
        }
        
    }

    void RollingResult(string result)
    {
        firstRollResult.text = result;
        firstRollResult.gameObject.SetActive(true);
        pressAnyKey.text = "-Press any key-";
        pressAnyKey.gameObject.SetActive(true);
        finalBet = 0;
        startRoll.SetActive(false);
        waitKey = true;
    }

    public void EraseDices()
    {
        for(int i=0; i < dices.Length; i++)
        {
            dices[i].SetActive(false);
            opponentDices[i].SetActive(false);
        }
    }

    public void Exit()
    {
        print("exit meoan");
        exitButton.SetActive(false);
        actualBet.text = "0";
        uiManagerScript.MeoanIsOn = false;
        dialogueSetupScript.ExitDialogue();
        pressAnyKey.gameObject.SetActive(false);
        board.SetActive(false);
    }
}

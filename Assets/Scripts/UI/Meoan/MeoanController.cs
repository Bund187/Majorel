using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeoanController : MonoBehaviour {

    public GameObject totalScore, roundScore, rollDice, nextTurn, winPanel,presskey, canvas,initialSetup,initialScore, exitButton,shake;
    public Text playersTotalScore, opponentTotalScore, playersRoundScore, opponentRoundScore, informationLabel,winnerInfo,winnerAmount, finalBet,ownGold;
    public GameObject[] dices, opponentDices;
    public MeoanSetUp setup;
    public PlayerController playerController;
    public DialogueSetupManager dialogueSetupScript;
    public Dialogue_UIManager uiManagerScript;
    bool opponentStarts, waitKey,isTheEnd,addGold, pressY, pressX, pressB;
    int riskMeter, winQty;
    int[] aux = new int[3];
    float time;

    private void Awake()
    {
        winQty = 30;
        riskMeter = 0;
    }

    void Update () {
        if (!isTheEnd)
        {
            addGold = true;
            initialScore.SetActive(true);
            roundScore.SetActive(true);

            if (opponentStarts)
            {
                OpponentsTurn();
            }
            if (waitKey)
                Wait();
        }
        if (int.Parse(playersTotalScore.text) >= winQty || int.Parse(opponentTotalScore.text) >= winQty)
        {
            isTheEnd = true;
            FinishGame();
            if (Input.GetAxis("Cancel")>0)
               ResetExitGame();
        }
        GamePadControl();

    }

    void OpponentsTurn()
    {
        OpponentAI();
    }

    public void ThrowDices(bool isOpponent)
    {
        presskey.SetActive(false);
        EraseDices();
        informationLabel.gameObject.SetActive(false);
        if (isOpponent)
        {
            //DicesResult(opponentDices, opponentRoundScore);
            CalculateDices(opponentDices, opponentRoundScore);
        }
        else
        {
            //DicesResult(dices, playersRoundScore);
            CalculateDices(dices, playersRoundScore);
        }
    }

    void CalculateDices(GameObject[] anyDices, Text anyRoundScore)
    {
        int[] results = new int[3];
        aux[0] = -1;
        aux[1] = -1;
        aux[2] = -1;
        bool fail = false, luck = false;
        int j = 0;
        while (j < 3)
        {
            //CALCULAMOS LOS DADOS Y LOS LANZAMOS
            results[j] = Random.Range(0, 6);

            //SI EL DADO SACADO NO ES IGUAL A LOS ANTERIORES
            if (results[j] != aux[0] && results[j] != aux[1])
            {
                anyDices[results[j]].SetActive(true);

                //SI SALE UN 3 FAIL, SI SALE UN 1 LUCK
                if (results[j] == 2)
                    fail = true;
                if (results[j] == 0)
                    luck = true;
                aux[j] = results[j];
                j++;
            }
        }

        //SI HA SALIDO UN 3 Y NINGUN 1
        if (fail && !luck)
        {
            informationLabel.text = "BAD LUCK";
            informationLabel.gameObject.SetActive(true);
            anyRoundScore.text = "0";
            shake.SetActive(false);
            rollDice.SetActive(false);
            if (anyDices[0].name.Contains("Other"))
            {
                informationLabel.text = "Opponent fail";
                presskey.SetActive(true);
                presskey.GetComponent<Text>().text = "Your turn";
                Pass();
            }
            else
                nextTurn.SetActive(true);
        }
        else
        {
            //VOLVEMIOS A RECORRER LOS NUMEROS QUE HAN SALIDO
            for (int i = 0; i < results.Length; i++)
            {
                //si el numero es par
                if ((results[i] + 1) % 2 == 0)
                {
                    if (luck)
                    {
                        informationLabel.text = "LUCKY ONE";
                        informationLabel.gameObject.SetActive(true);
                    }
                    //SUMAMOS EL RESULTADO
                    anyRoundScore.text = (int.Parse(anyRoundScore.text) + results[i] + 1).ToString();
                }
                //SI NO HA SALIDO UN 3 Y HA SALIDO UN 1
                if (!fail && luck)
                {
                    informationLabel.text = "LUCKY ONE";
                    informationLabel.gameObject.SetActive(true);
                    if (anyRoundScore.gameObject.name.Contains("Opponent"))
                    {
                        //RESTAMOS 3 AL PLAYER EN CASO DE QUE TENGA 3 O MAS (PARA QUE NO NOS DE NEGATIVO)
                        if (int.Parse(playersTotalScore.text) >= 3)
                        {
                            playersTotalScore.text = (int.Parse(playersTotalScore.text) - 3).ToString();
                            luck = false;
                        }
                        else
                        {
                            playersTotalScore.text = "0";
                            luck = false;
                        }
                    }
                    else
                    {
                        //RESTAMOS 3 AL OPONENTE EN CASO DE QUE TENGA 3 O MAS (PARA QUE NO NOS DE NEGATIVO)
                        if (int.Parse(opponentTotalScore.text) >= 3)
                        {
                            opponentTotalScore.text = (int.Parse(opponentTotalScore.text) - 3).ToString();
                            luck = false;
                        }
                        else
                        {
                            opponentTotalScore.text = "0";
                            luck = false;
                        }
                    }
                }
            }
        }
    }

    //void DicesResult(GameObject[] anyDices, Text anyRoundScore)
    //{
    //    int i = 0;
    //    int rnd;
        
    //    while (i < 3)
    //    {
    //        rnd = Random.Range(0, 6);
    //        if (rnd != aux[0] && rnd!=aux[1])
    //        {
    //            anyDices[rnd].SetActive(true);
    //            if ((rnd + 1) % 2 == 0 && rnd != 2)
    //            {
    //                anyRoundScore.text = (int.Parse(anyRoundScore.text) + rnd + 1).ToString();
    //            }
    //            else if (rnd == 2)
    //            {
    //                informationLabel.text = "BAD LUCK";
    //                informationLabel.gameObject.SetActive(true);
    //                anyRoundScore.text = "0";
    //                shake.SetActive(false);
    //                rollDice.SetActive(false);
    //                if (anyDices[0].name.Contains("Other"))
    //                {
    //                    informationLabel.text = "Opponent fail";
    //                    presskey.SetActive(true);
    //                    presskey.GetComponent<Text>().text = "Your turn";
    //                    Pass();
    //                }
    //                else
    //                    nextTurn.SetActive(true);
                    
    //            }
    //            else if(rnd == 0)
    //            {
    //                informationLabel.text = "LUCKY ONE";
    //                informationLabel.gameObject.SetActive(true);
    //                if (anyRoundScore.gameObject.name.Contains("Opponent"))
    //                {

    //                    if (int.Parse(playersTotalScore.text) >= 3)
    //                    {
    //                        print("restamos 3 al player");
    //                        playersTotalScore.text = (int.Parse(playersTotalScore.text) - 3).ToString();
    //                    }
    //                    else
    //                        playersTotalScore.text = "0";
    //                }
    //                else
    //                {
    //                    if (int.Parse(opponentTotalScore.text) >= 3)
    //                        opponentTotalScore.text = (int.Parse(opponentTotalScore.text) - 3).ToString();
    //                    else
    //                        opponentTotalScore.text = "0";
    //                }
    //            }
    //            aux[i] = rnd;
    //            i++;
    //        }
    //    }
    //}

    public void Pass()
    {
        //int i = 0;
        //bool points=false;
        //while (i < aux.Length)
        //{
        //    if (aux[i] == 2)
        //    {
        //        i = aux.Length;
        //        points = true;
        //    }
        //    else
        //    {
        //        i++;
        //    }
        //}
        //if (!points)
        //{
            playersTotalScore.text = (int.Parse(playersRoundScore.text) + int.Parse(playersTotalScore.text)).ToString();
            opponentTotalScore.text = (int.Parse(opponentRoundScore.text) + int.Parse(opponentTotalScore.text)).ToString();
        //}
        SwapPlayer();
        riskMeter = 0;
    }

    void SwapPlayer()
    {
        opponentRoundScore.text = "0";
        playersRoundScore.text = "0";
        rollDice.SetActive(false);
        shake.SetActive(false);
        nextTurn.SetActive(false);
        opponentStarts = !opponentStarts;
        
    }

    void OpponentAI()
    {
        if (!waitKey && int.Parse(playersTotalScore.text) < winQty && int.Parse(opponentTotalScore.text) < winQty)
        {
            int i = 0;
            int quit = -1;
            while (i < riskMeter && quit != 0)
            {
                quit = Random.Range(0, 4);
                i++;
            }
            if (quit != 0 && ((int.Parse(opponentTotalScore.text)+int.Parse(opponentRoundScore.text))<30))
            {
                ThrowDices(true);
            }
            else
            {
                informationLabel.text = "opponent \n passes";
                informationLabel.gameObject.SetActive(true);
                Pass();
            }
            riskMeter++;
            waitKey = true;
            time = Time.time;
        }
    }

    void Wait()
    {
        if (Time.time > time + 1.5f)
        {
            EraseDices();
            informationLabel.gameObject.SetActive(false);
            waitKey = false;
            if (!opponentStarts)
            {
                opponentRoundScore.text = "0";
                shake.SetActive(true);
                rollDice.SetActive(true);
                nextTurn.SetActive(true);
            }
        }
    }

    void FinishGame()
    {
        EraseDices();
        rollDice.SetActive(false);
        shake.SetActive(false);
        nextTurn.SetActive(false);
        initialScore.SetActive(false);
        roundScore.SetActive(false);
        presskey.SetActive(false);
        informationLabel.gameObject.SetActive(false);
        exitButton.SetActive(true);
        winPanel.SetActive(true);
        if (int.Parse(playersTotalScore.text) >= winQty)
        {
            winnerInfo.text = "You win";
            winnerAmount.text = "You earn " + (int.Parse(finalBet.text)/2) + " gold";
            winnerAmount.gameObject.SetActive(true);
            opponentTotalScore.text = "0";
            if (addGold)
            {
                ownGold.text = (int.Parse(ownGold.text) + int.Parse(finalBet.text)).ToString();
            }
            addGold = false;
        }
        if(int.Parse(opponentTotalScore.text) >= winQty)
        {
            winnerInfo.text = "You lose";
            playersTotalScore.text = "0";
        }
        winnerInfo.gameObject.SetActive(true);
    }

    public void ResetExitGame()
    {
        exitButton.SetActive(false);
        playerController.IsTalking = false;
        playersRoundScore.text = "0";
        opponentRoundScore.text = "0";
        playersTotalScore.text = "0";
        opponentTotalScore.text = "0";
        finalBet.text = "0";
        winPanel.SetActive(false);
        winnerInfo.gameObject.SetActive(false);
        winnerAmount.gameObject.SetActive(false);
        EraseDices();
        canvas.SetActive(false);
        initialSetup.SetActive(true);
        setup.enabled = true;
        uiManagerScript.MeoanIsOn = false;
        dialogueSetupScript.ExitDialogue();
    }

    void GamePadControl()
    {
        //Pulsar la Y
        if (Input.GetAxisRaw("Menu") != 0)
        {
            if (!pressY)
            {
                pressY = true;
                if (rollDice.activeSelf)
                    ThrowDices(false);
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
                if (nextTurn.activeSelf)
                    Pass();
            }
        }
        else
        {
            pressX = false;

        }

        //Pulsar la B
        if (Input.GetAxisRaw("Cancel") != 0)
        {
            if (!pressB)
            {
                pressB = true;
                shake.GetComponent<Animator>().SetTrigger("Shake");
            }
        }
        else
        {
            pressB = false;

        }
    }

    public void EraseDices()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].SetActive(false);
            opponentDices[i].SetActive(false);
        }
    }
    
    public bool OpponentStarts
    {
        get
        {
            return opponentStarts;
        }

        set
        {
            opponentStarts = value;
        }
    }

    public bool IsTheEnd
    {
        get
        {
            return isTheEnd;
        }

        set
        {
            isTheEnd = value;
        }
    }
}

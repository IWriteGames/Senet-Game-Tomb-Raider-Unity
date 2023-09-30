using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager Instance;

    //Values
    public bool turnPlayer;
    public bool turnIA;
    public bool gameFinished = false;
    public bool canRoll = false;
    public bool canMoveButtons = false;
    public int numberResult;
    public int pieceFinishedPlayer = 0;
    public int pieceFinishedAI = 0;


    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        if(Random.Range(0, 2) == 0)
        {
            turnPlayer = true;
            canRoll = true;
            turnIA = false;
        } else {
            turnPlayer = false;
            turnIA = true;
            TurnEnemy();
        }
    }

    public void TurnPlayer()
    {
        turnPlayer = true;
        canRoll = true;
        canMoveButtons = false;
        turnIA = false;
    }

    public void TurnEnemy()
    {
        turnPlayer = false;
        canRoll = false;
        canMoveButtons = false;
        turnIA = true;
        TokenManager.Instance.MoveTokens(false);
        CheckerPieces.Instance.NextIAPiece();
    }

    public void ActiveButtonsPlayer()
    {
        canRoll = false;
        canMoveButtons = true;
    }

    public void DeactiveButtonsPlayer()
    {
        canMoveButtons = false;
    } 

}

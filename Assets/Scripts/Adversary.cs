using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Adversary : MonoBehaviour
{
    public static Adversary Instance;
    [SerializeField] private GameObject pieceOne;
    [SerializeField] private GameObject pieceTwo;
    [SerializeField] private GameObject pieceThree;

    private bool pieceOneActive;
    private bool pieceTwoActive;
    private bool pieceThreeActive;

    private bool pieceOneCanMove;
    private bool pieceTwoCanMove;
    private bool pieceThreeCanMove;


    private void Awake() 
    {
        Instance = this;

        pieceOneActive = true;
        pieceTwoActive = true;
        pieceThreeActive = true;        
    }

    public void TurnAdversary()
    {
        bool IAMove = true;
        bool pieceChoosed = true;
        GameManager.Instance.numberResult = TokenManager.Instance.MoveTokens(false);
        if(!checkPossibilities(GameManager.Instance.numberResult))
        {
            // Debug.Log("IA don't have possibilities");
            GameManager.Instance.turnPlayer = true;
            GameManager.Instance.canRoll = true;
            return;
        }
        // Debug.Log("IA Turn");
        // Debug.Log(GameManager.Instance.numberResult);


        int numberResult = GameManager.Instance.numberResult;
        int piecePosition = 0;
        int nextPosition = 0;

        if(GameManager.Instance.piece1AIActive && pieceChoosed)
        {
            piecePosition = pieceOne.GetComponent<Piece>().actualPosition;
            nextPosition = piecePosition + numberResult;
            pieceOneCanMove = true;
            if(nextPosition <= 16)
            {
                pieceChoosed = false;
            } else {
                pieceOneCanMove = false;
            }
        } 

        if(GameManager.Instance.piece2AIActive && pieceChoosed)
        {
            piecePosition = pieceTwo.GetComponent<Piece>().actualPosition;
            nextPosition = piecePosition + numberResult;
            pieceTwoCanMove = true;
            if(nextPosition <= 16)
            {
                pieceChoosed = false;
            } else {
                pieceTwoCanMove = false;
            }
        } 
        
        if(GameManager.Instance.piece3AIActive && pieceChoosed)
        {
            piecePosition = pieceThree.GetComponent<Piece>().actualPosition;
            nextPosition = piecePosition + numberResult;
            pieceThreeCanMove = true;
            if(nextPosition <= 16)
            {
                pieceChoosed = false;
            } else {
                pieceThreeCanMove = false;
            }
        } 

        if(nextPosition >= 17)
        {
            if(!checkPossibilities(nextPosition))
            {
                GameManager.Instance.turnPlayer = true;
                GameManager.Instance.canRoll = true;
                return;
            }
        }

        GameObject nextCell = TableGame.Instance.getPointMove(false, nextPosition);
        //Check if Exists
        bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
        bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
        bool occupiedByIA = nextCell.GetComponent<CellTable>().occupiedByIA;
        bool isLastCell = nextCell.GetComponent<CellTable>().isLastCell;
        GameObject enemyPieceCell = nextCell.GetComponent<CellTable>().pieceInTheCell;

        bool continuePiece1 = false;
        bool continuePiece2 = false;
        bool continuePiece3 = false;


        if(!GameManager.Instance.turnPlayer && GameManager.Instance.piece1AIActive && IAMove && pieceOneCanMove) 
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece1 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByIA && !isSafeCell) { 

                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetPlayerPiece(positionEnemyPieceCell).transform.position;
                continuePiece1 = true; 
            }

            if(continuePiece1)
            {
                if(TableGame.Instance.getPointMove(false, nextPosition) is not null)
                {
                    pieceOne.transform.position = TableGame.Instance.getPointMove(false, nextPosition).transform.position;
                    pieceOne.GetComponent<Piece>().actualPosition = nextPosition;
                    nextCell.GetComponent<CellTable>().pieceInTheCell = pieceOne;
                    nextCell.GetComponent<CellTable>().isEmpty = false;
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                    nextCell.GetComponent<CellTable>().occupiedByIA = true;
                    IAMove = false;
                }
            }

            if(isLastCell) {
                GameManager.Instance.pieceFinishedAI = GameManager.Instance.pieceFinishedAI + 1;
                pieceOne.SetActive(false);
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.piece1AIActive = false;
            }
        }

        if(!GameManager.Instance.turnPlayer && GameManager.Instance.piece2AIActive && IAMove && pieceTwoCanMove) 
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece2 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByIA && !isSafeCell) { 
                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetPlayerPiece(positionEnemyPieceCell).transform.position;

                continuePiece2 = true; 
            }

            if(continuePiece2)
            {
                if(TableGame.Instance.getPointMove(false, nextPosition) is not null)
                {
                    pieceTwo.transform.position = TableGame.Instance.getPointMove(false, nextPosition).transform.position;
                    pieceTwo.GetComponent<Piece>().actualPosition = nextPosition;
                    nextCell.GetComponent<CellTable>().pieceInTheCell = pieceTwo;
                    nextCell.GetComponent<CellTable>().isEmpty = false;
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                    nextCell.GetComponent<CellTable>().occupiedByIA = true;
                    IAMove = false;
                }
            }

            if(isLastCell) {
                GameManager.Instance.pieceFinishedAI = GameManager.Instance.pieceFinishedAI + 1;
                pieceTwo.SetActive(false);
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.piece2AIActive = false;
            }


        }

        if(!GameManager.Instance.turnPlayer && GameManager.Instance.piece3AIActive && IAMove && pieceThreeCanMove) 
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece3 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByIA && !isSafeCell) { 
                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetPlayerPiece(positionEnemyPieceCell).transform.position;

                continuePiece3 = true; 
            }

            if(continuePiece3)
            {
                if(TableGame.Instance.getPointMove(false, nextPosition) is not null)
                {
                    pieceThree.transform.position = TableGame.Instance.getPointMove(false, nextPosition).transform.position;
                    pieceThree.GetComponent<Piece>().actualPosition = nextPosition;
                    nextCell.GetComponent<CellTable>().pieceInTheCell = pieceThree;
                    nextCell.GetComponent<CellTable>().isEmpty = false;
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                    nextCell.GetComponent<CellTable>().occupiedByIA = true;
                    IAMove = false;
                }
            }
            
            if(isLastCell) {
                GameManager.Instance.pieceFinishedAI = GameManager.Instance.pieceFinishedAI + 1;
                pieceThree.SetActive(false);
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.piece3AIActive = false;
            }
        }


        //Player Victory
        if(GameManager.Instance.pieceFinishedAI == 3)
        {
            PlayerController.Instance.openFinishedGame(false);
        }

        if(numberResult == 0 || isSafeCell)
        {
            GameManager.Instance.turnPlayer = false;
            TurnAdversary();
        } 

        if(IAMove)
            {
                //TODO
                // Debug.Log("IA Can't Move");
                GameManager.Instance.turnPlayer = true;
                GameManager.Instance.canRoll = true;
            } else {
                //Reset Last Cell
                if(piecePosition != 0)
                {
                    GameObject lastCell = TableGame.Instance.getPointMove(false, piecePosition);
                    lastCell.GetComponent<CellTable>().pieceInTheCell = null;
                    lastCell.GetComponent<CellTable>().isEmpty = true;
                    lastCell.GetComponent<CellTable>().occupiedByPlayer = false;
                }

                GameManager.Instance.turnPlayer = true;
                GameManager.Instance.canRoll = true;
            }
        }

    public bool checkPossibilities(int numberResult)
    {
        int piecePosition;
        if(numberResult == 0)
        {
            numberResult = 6;
        }

        if(pieceOneActive) 
        {
            piecePosition = pieceOne.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;
            GameObject nextCell = TableGame.Instance.getPointMove(false, nextPosition);
            if(nextCell is not null)
            {
                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByIA = nextCell.GetComponent<CellTable>().occupiedByIA;
                //If is Empty Continue
                if(isEmpty) { return true; }

                //Destroy EnemyPiece
                if(!isEmpty && !occupiedByIA && !isSafeCell) { return true; }
            }
        }

        if(pieceTwoActive) 
        {
            piecePosition = pieceTwo.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;
            GameObject nextCell = TableGame.Instance.getPointMove(false, nextPosition);

            if(nextCell is not null)
            {
                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByIA = nextCell.GetComponent<CellTable>().occupiedByIA;
                //If is Empty Continue
                if(isEmpty) { return true; }

                //Destroy EnemyPiece
                if(!isEmpty && !occupiedByIA && !isSafeCell) { return true; }
            }
        }

        if(pieceThreeActive) 
        {
            piecePosition = pieceThree.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;
            GameObject nextCell = TableGame.Instance.getPointMove(false, nextPosition);

            if(nextCell is not null)
            {
                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByIA = nextCell.GetComponent<CellTable>().occupiedByIA;
                //If is Empty Continue
                if(isEmpty) { return true; }

                //Destroy EnemyPiece
                if(!isEmpty && !occupiedByIA && !isSafeCell) { return true; }
            }
        }
        return false;
    }

}



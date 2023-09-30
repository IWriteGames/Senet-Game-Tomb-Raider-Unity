using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePieces : MonoBehaviour
{
    public static MovePieces Instance;

    //Pieces
    [SerializeField] private GameObject pieceRed; //3
    [SerializeField] private GameObject pieceGreen; //2
    [SerializeField] private GameObject pieceBlue; //1

    private bool pieceRedActive;
    private bool pieceGreenActive;
    private bool pieceBlueActive;


    private void Awake() 
    {
        Instance = this;
        pieceRedActive = true;
        pieceGreenActive = true;
        pieceBlueActive = true;
    }

    public void Start()
    {
        pieceBlue.transform.position = TableGame.Instance.playerStartBlue.transform.position;
        pieceGreen.transform.position = TableGame.Instance.playerStartGreen.transform.position;
        pieceRed.transform.position = TableGame.Instance.playerStartRed.transform.position;
    }

    public void MovePiece(int piece)
    {


        GameManager.Instance.canMoveButtons = false;
        int numberResult = GameManager.Instance.numberResult;
    
        if(!MovePieces.Instance.checkPossibilities(numberResult))
        {
            GameManager.Instance.canRoll = false;
            GameManager.Instance.canMoveButtons = false;
            GameManager.Instance.turnPlayer = false;
            Adversary.Instance.TurnAdversary();
        }

        int movements = numberResult;

        if(movements == 0)
        {
            movements = 6;
        }

        int piecePosition = 0;

        if(piece == 1)
        {
            if(!GameManager.Instance.pieceBlueActive)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }
            piecePosition = pieceBlue.GetComponent<Piece>().actualPosition;
        } 
        else if(piece == 2)
        {
            if(!GameManager.Instance.pieceGreenActive)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }
            piecePosition = pieceGreen.GetComponent<Piece>().actualPosition;
        } 
        else if(piece == 3)
        {
            if(!GameManager.Instance.pieceRedActive)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }
            piecePosition = pieceRed.GetComponent<Piece>().actualPosition;
        } 


        int nextPosition = piecePosition + movements;
        if(nextPosition >= 17)
        {
            GameManager.Instance.canMoveButtons = true;
            return;
        }

        GameObject nextCell = TableGame.Instance.getPointMove(true, nextPosition);

        bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
        bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
        bool occupiedByPlayer = nextCell.GetComponent<CellTable>().occupiedByPlayer;
        bool isLastCell = nextCell.GetComponent<CellTable>().isLastCell;
        GameObject enemyPieceCell = nextCell.GetComponent<CellTable>().pieceInTheCell;

        bool continuePiece1 = false;
        bool continuePiece2 = false;
        bool continuePiece3 = false;


        if(piece == 1)
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece1 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByPlayer && !isSafeCell) { 

                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetAIPiece(positionEnemyPieceCell).transform.position;
                continuePiece1 = true; 
            }

            if(!isEmpty && occupiedByPlayer)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }

            if(isLastCell) {
                GameManager.Instance.pieceFinishedPlayer = GameManager.Instance.pieceFinishedPlayer + 1;
                pieceBlue.SetActive(false);
                pieceBlueActive = false;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.pieceBlueActive = false;
            }



            if(continuePiece1)
            {
                pieceBlue.transform.position = TableGame.Instance.getPointMove(true, nextPosition).transform.position;
                pieceBlue.GetComponent<Piece>().actualPosition = nextPosition;
                nextCell.GetComponent<CellTable>().pieceInTheCell = pieceBlue;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = true;
                if(isLastCell)
                {
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                }
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
            }
        }

        if(piece == 2)
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece2 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByPlayer && !isSafeCell) { 
                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetAIPiece(positionEnemyPieceCell).transform.position;
                continuePiece2 = true; 
            }

            if(!isEmpty && occupiedByPlayer)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }

            if(isLastCell) {
                GameManager.Instance.pieceFinishedPlayer = GameManager.Instance.pieceFinishedPlayer + 1;
                pieceGreen.SetActive(false);
                pieceGreenActive = false;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.pieceGreenActive = false;
            }


            if(continuePiece2)
            {
                pieceGreen.transform.position = TableGame.Instance.getPointMove(true, nextPosition).transform.position;
                pieceGreen.GetComponent<Piece>().actualPosition = nextPosition;
                nextCell.GetComponent<CellTable>().pieceInTheCell = pieceGreen;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = true;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                if(isLastCell)
                {
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                }

            }
        }
        
        if(piece == 3)
        {
            //If is Empty Continue
            if(isEmpty) { continuePiece3 = true; }

            //Destroy EnemyPiece
            if(!isEmpty && !occupiedByPlayer && !isSafeCell) { 
                int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
                enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
                enemyPieceCell.transform.position = TableGame.Instance.ResetAIPiece(positionEnemyPieceCell).transform.position;
                continuePiece3 = true; 
            }

            if(!isEmpty && occupiedByPlayer)
            {
                GameManager.Instance.canMoveButtons = true;
                return;
            }

            if(isLastCell) {
                GameManager.Instance.pieceFinishedPlayer = GameManager.Instance.pieceFinishedPlayer + 1;
                pieceRed.SetActive(false);
                pieceRedActive = false;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                GameManager.Instance.pieceRedActive = false;
            }

            if(continuePiece3)
            {
                pieceRed.transform.position = TableGame.Instance.getPointMove(true, nextPosition).transform.position;
                pieceRed.GetComponent<Piece>().actualPosition = nextPosition;
                nextCell.GetComponent<CellTable>().pieceInTheCell = pieceRed;
                nextCell.GetComponent<CellTable>().isEmpty = false;
                nextCell.GetComponent<CellTable>().occupiedByPlayer = true;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
                if(isLastCell)
                {
                    nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                }
            }
        }


        //Player Victory
        if(GameManager.Instance.pieceFinishedPlayer == 3)
        {
            PlayerController.Instance.openFinishedGame(true);
        }

        //Reset Last Cell
        if(piecePosition != 0)
        {
            GameObject lastCell = TableGame.Instance.getPointMove(true, piecePosition);
            lastCell.GetComponent<CellTable>().pieceInTheCell = null;
            lastCell.GetComponent<CellTable>().isEmpty = true;
            lastCell.GetComponent<CellTable>().occupiedByPlayer = false;
        }

        if(numberResult == 0 || isSafeCell)
        {
            GameManager.Instance.turnPlayer = true;
            GameManager.Instance.canRoll = true;
        } else {
            GameManager.Instance.turnPlayer = false;
            Adversary.Instance.TurnAdversary();
        }
    }



    public bool checkPossibilities(int numberResult)
    {
        int piecePosition;
        if(numberResult == 0)
        {
            numberResult = 6;
        }

        if(pieceBlueActive) 
        {
            piecePosition = pieceBlue.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;

            if(nextPosition <= 16)
            {
                GameObject nextCell = TableGame.Instance.getPointMove(true, nextPosition);

                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByPlayer = nextCell.GetComponent<CellTable>().occupiedByPlayer;

                if(nextCell is not null)
                {
                    //If is Empty Continue
                    if(isEmpty) { return true; }

                    //Destroy EnemyPiece
                    if(!isEmpty && !occupiedByPlayer && !isSafeCell) { return true; }
                }
            }


        }

        if(pieceGreenActive) 
        {
            piecePosition = pieceGreen.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;

            if(nextPosition <= 16)
            {

                GameObject nextCell = TableGame.Instance.getPointMove(true, nextPosition);

                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByPlayer = nextCell.GetComponent<CellTable>().occupiedByPlayer;

                if(nextCell is not null)
                {
                    //If is Empty Continue
                    if(isEmpty) { return true; }

                    //Destroy EnemyPiece
                    if(!isEmpty && !occupiedByPlayer && !isSafeCell) { return true; }
                }
            }
        }

        if(pieceRedActive) 
        {
            piecePosition = pieceRed.GetComponent<Piece>().actualPosition;
            int nextPosition = piecePosition + numberResult;

            if(nextPosition <= 16)
            {

                GameObject nextCell = TableGame.Instance.getPointMove(true, nextPosition);

                bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
                bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
                bool occupiedByPlayer = nextCell.GetComponent<CellTable>().occupiedByPlayer;

                if(nextCell is not null)
                {
                    //If is Empty Continue
                    if(isEmpty) { return true; }

                    //Destroy EnemyPiece
                    if(!isEmpty && !occupiedByPlayer && !isSafeCell) { return true; }
                }
            }
        }
        return false;
    }

}

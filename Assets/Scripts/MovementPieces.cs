using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementPieces : MonoBehaviour
{
    public static MovementPieces Instance;

    private bool canMove;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        InitialPiecePosition();
    }

    public void InitialPiecePosition()
    {
        //Player
        CheckerPieces.Instance.pieceBlue.transform.position = TableGame.Instance.playerStartBlue.transform.position;
        CheckerPieces.Instance.pieceGreen.transform.position = TableGame.Instance.playerStartGreen.transform.position;
        CheckerPieces.Instance.pieceRed.transform.position = TableGame.Instance.playerStartRed.transform.position;

        //IA
        CheckerPieces.Instance.pieceOne.transform.position = TableGame.Instance.startAI1.transform.position;
        CheckerPieces.Instance.pieceTwo.transform.position = TableGame.Instance.startAI2.transform.position;
        CheckerPieces.Instance.pieceThree.transform.position = TableGame.Instance.startAI3.transform.position;
    }


    public void MovePiece(int numberPiece, bool isPlayer)
    {
        GameManager.Instance.DeactiveButtonsPlayer();
        int numberResult = GameManager.Instance.numberResult;
        GameObject piece = CheckerPieces.Instance.ReturnPiece(numberPiece, isPlayer);
        
        //Check Possibilities
        canMove = CheckerPieces.Instance.CheckPossibilitiesPerPiece(piece, numberResult, isPlayer);
        if(!canMove) { GameManager.Instance.ActiveButtonsPlayer(); return; }

        //Check if is Active
        canMove = CheckerPieces.Instance.CheckPieceActive(numberPiece, isPlayer);
        if(!canMove) { GameManager.Instance.ActiveButtonsPlayer(); return; }

        //Calculate next Movement
        int actualPiecePosition = CheckerPieces.Instance.PositionPiece(numberPiece, isPlayer);
        int nextPosition = actualPiecePosition + numberResult;

        //Animation
        StartCoroutine(MovePiecesAnim(
            isPlayer, piece, TableGame.Instance.GetPointMove(isPlayer, nextPosition), numberPiece, nextPosition, actualPiecePosition, numberResult
        ));
    }

    public IEnumerator MovePiecesAnim(bool isPlayer, GameObject Piece, GameObject positionTable, int numberPiece, int nextPosition, int actualPiecePosition, int numberResult)
    {
        bool movement = true;
        while (movement) 
        {
            Piece.transform.position = Vector3.MoveTowards(Piece.transform.position, positionTable.transform.position, 0.2f);
            if(Piece.transform.position == positionTable.transform.position)
            {
                movement = false;
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        StartCoroutine(InfoMovementPiece(isPlayer, numberPiece, Piece, nextPosition, actualPiecePosition, numberResult, positionTable));
    }

    public IEnumerator InfoMovementPiece(bool isPlayer, int numberPiece, GameObject piece, int nextPosition, int actualPiecePosition, int numberResult, GameObject positionTable)
    {
        piece.transform.position = positionTable.transform.position;

        //Values from Next Cell
        GameObject nextCell = TableGame.Instance.GetPointMove(isPlayer, nextPosition);
        bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
        bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;
        bool occupiedByOther;

        if(isPlayer)
        {
            occupiedByOther = nextCell.GetComponent<CellTable>().occupiedByPlayer;
        } else {
            occupiedByOther = nextCell.GetComponent<CellTable>().occupiedByIA;
        }

        bool isLastCell = nextCell.GetComponent<CellTable>().isLastCell;
        GameObject enemyPieceCell = nextCell.GetComponent<CellTable>().pieceInTheCell;


        //Destroy EnemyPiece
        if(!isEmpty && !occupiedByOther && !isSafeCell && !isLastCell) 
        { 
            int positionEnemyPieceCell = enemyPieceCell.GetComponent<Piece>().numberPiece;
            enemyPieceCell.GetComponent<Piece>().actualPosition = 0;
            enemyPieceCell.transform.position = TableGame.Instance.ResetPiecePosition(positionEnemyPieceCell, isPlayer).transform.position;
        }

        //Check Last Cell
        if(isLastCell) 
        {
            if(isPlayer)
            {
                GameManager.Instance.pieceFinishedPlayer++;
            } else {
                GameManager.Instance.pieceFinishedAI++;
            }
            piece.SetActive(false);
            CheckerPieces.Instance.FinishPieceActive(numberPiece, isPlayer);
            nextCell.GetComponent<CellTable>().isEmpty = false;
            nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
            nextCell.GetComponent<CellTable>().occupiedByIA = false;

        //Values for next Cell
        } else {
            nextCell.GetComponent<CellTable>().pieceInTheCell = piece;
            nextCell.GetComponent<CellTable>().isEmpty = false;
            if(isPlayer)
            {
                nextCell.GetComponent<CellTable>().occupiedByPlayer = true;
                nextCell.GetComponent<CellTable>().occupiedByIA = false;
            } else {
                nextCell.GetComponent<CellTable>().occupiedByPlayer = false;
                nextCell.GetComponent<CellTable>().occupiedByIA = true;
            }

        }
        
        //Update Position Piece
        piece.GetComponent<Piece>().actualPosition = nextPosition;


        //Player Victory
        if(GameManager.Instance.pieceFinishedPlayer == 3)
        {
            PlayerController.Instance.OpenFinishedGame(true);
            GameManager.Instance.gameFinished = true;
        }

        //Enemy Victory
        if(GameManager.Instance.pieceFinishedAI == 3)
        {
            PlayerController.Instance.OpenFinishedGame(false);
            GameManager.Instance.gameFinished = true;
        }

        if(!GameManager.Instance.gameFinished)
        {
            //Reset Last Cell
            if(actualPiecePosition != 0)
            {
                GameObject lastCell = TableGame.Instance.GetPointMove(isPlayer, actualPiecePosition);
                lastCell.GetComponent<CellTable>().pieceInTheCell = null;
                lastCell.GetComponent<CellTable>().isEmpty = true;
                lastCell.GetComponent<CellTable>().occupiedByPlayer = false;
                lastCell.GetComponent<CellTable>().occupiedByIA = false;
            }

            //Next
            if(isPlayer)
            {
                if(numberResult == 6 || isSafeCell)
                {
                    GameManager.Instance.TurnPlayer();
                } else {
                    GameManager.Instance.TurnEnemy();
                }
            } else {
                if(numberResult == 6 || isSafeCell)
                {
                    GameManager.Instance.TurnEnemy();
                } else {
                    GameManager.Instance.TurnPlayer();
                }
            }
        }

        yield return null;
    }

}


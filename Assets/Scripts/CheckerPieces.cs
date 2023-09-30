using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CheckerPieces : MonoBehaviour
{
    public static CheckerPieces Instance;

    //Pieces
    public GameObject pieceBlue; //1
    public GameObject pieceGreen; //2
    public GameObject pieceRed; //3
    public GameObject pieceOne;
    public GameObject pieceTwo;
    public GameObject pieceThree;

    //Player States Pieces
    private bool pieceBlueActive;
    private bool pieceGreenActive;
    private bool pieceRedActive;

    //IA States Pieces
    private bool pieceOneActive;
    private bool pieceTwoActive;
    private bool pieceThreeActive;

    private void Awake() 
    {
        Instance = this;

        pieceRedActive = true;
        pieceGreenActive = true;
        pieceBlueActive = true;

        pieceOneActive = true;
        pieceTwoActive = true;
        pieceThreeActive = true;
    }

    public bool CheckPieceActive(int numberPiece, bool isPlayer)
    {
        if(isPlayer)
        {
            if(numberPiece == 1)
            {
                return pieceBlueActive;
            } 
            else if(numberPiece == 2)
            {
                return pieceGreenActive;
            } 
            else if(numberPiece == 3)
            {
                return pieceRedActive;
            }
        } else {
            if(numberPiece == 1)
            {
                return pieceOneActive;
            } 
            else if(numberPiece == 2)
            {
                return pieceTwoActive;
            } 
            else if(numberPiece == 3)
            {
                return pieceThreeActive;
            }
        }
        return false;
    }

    public void FinishPieceActive(int numberPiece, bool isPlayer)
    {
        if(isPlayer)
        {
            if(numberPiece == 1)
            {
                pieceBlueActive = false;
            } 
            else if(numberPiece == 2)
            {
                pieceGreenActive = false;
            } 
            else if(numberPiece == 3)
            {
                pieceRedActive = false;
            }
        } else {
            if(numberPiece == 1)
            {
                pieceOneActive = false;
            } 
            else if(numberPiece == 2)
            {
                pieceTwoActive = false;
            } 
            else if(numberPiece == 3)
            {
                pieceThreeActive = false;
            }
        }

    }

    public int PositionPiece(int numberPiece, bool isPlayer)
    {
        int piecePosition = 0;

        if(isPlayer)
        {
            if(numberPiece == 1)
            {
                piecePosition = pieceBlue.GetComponent<Piece>().actualPosition;
            } 
            else if(numberPiece == 2)
            {
                piecePosition = pieceGreen.GetComponent<Piece>().actualPosition;
            } 
            else if(numberPiece == 3)
            {
                piecePosition = pieceRed.GetComponent<Piece>().actualPosition;
            }
        } else {
            if(numberPiece == 1)
            {
                piecePosition = pieceOne.GetComponent<Piece>().actualPosition;
            } 
            else if(numberPiece == 2)
            {
                piecePosition = pieceTwo.GetComponent<Piece>().actualPosition;
            } 
            else if(numberPiece == 3)
            {
                piecePosition = pieceThree.GetComponent<Piece>().actualPosition;
            }
        }

        return piecePosition;
    }

    public GameObject ReturnPiece(int numberPiece, bool isPlayer)
    {
        if(isPlayer)
        {
            if(numberPiece == 1)
            {
                return pieceBlue;
            }
            else if(numberPiece == 2)
            {
                return pieceGreen;
            }
            else if(numberPiece == 3)
            {
                return pieceRed;
            }
        } else {
            if(numberPiece == 1)
            {
                return pieceOne;
            }
            else if(numberPiece == 2)
            {
                return pieceTwo;
            }
            else if(numberPiece == 3)
            {
                return pieceThree;
            }
        }

        return null;
    }

    public bool CheckPossibilitiesPerPiece(GameObject piece, int numberResult, bool isPlayer)
    {
        int piecePosition;
        bool occupiedByOther;

        piecePosition = piece.GetComponent<Piece>().actualPosition;
        int nextPosition = piecePosition + numberResult;

        if(nextPosition <= 16)
        {
            GameObject nextCell = TableGame.Instance.GetPointMove(true, nextPosition);

            bool isEmpty = nextCell.GetComponent<CellTable>().isEmpty;
            bool isSafeCell = nextCell.GetComponent<CellTable>().isSafeCell;

            if(isPlayer)
            {
                occupiedByOther = nextCell.GetComponent<CellTable>().occupiedByPlayer;
            } else {
                occupiedByOther = nextCell.GetComponent<CellTable>().occupiedByIA;
            }

            if(nextCell is not null)
            {
                //If is Empty Continue
                if(isEmpty) { return true; }

                //Destroy EnemyPiece
                if(!isEmpty && !occupiedByOther && !isSafeCell) { return true; }
            }
        }

        return false;
    }

    public void NextIAPiece()
    {
        bool activePieceAI;
        GameObject piece;

        for(int i = 1; i <= 3; i++)
        {
            activePieceAI = CheckPieceActive(i, false);

            if(activePieceAI)
            {
                piece = ReturnPiece(i, false);
                if(CheckPossibilitiesPerPiece(piece, GameManager.Instance.numberResult, false))
                {
                    MovementPieces.Instance.MovePiece(i, false);
                    return;
                }
            }
        }

        if(GameManager.Instance.numberResult == 6)
        {
            GameManager.Instance.TurnEnemy();
        } else {
            GameManager.Instance.TurnPlayer();
        }

    }

    public void AllPossibilitiesPlayer()
    {
        bool activePieceAI;
        GameObject piece;
        
        for(int i = 1; i <= 3; i++)
        {
            activePieceAI = CheckPieceActive(i, true);

            if(activePieceAI)
            {
                piece = ReturnPiece(i, true);
                if(CheckPossibilitiesPerPiece(piece, GameManager.Instance.numberResult, true))
                {
                    return;
                }
            }
        }

        if(GameManager.Instance.numberResult == 6) { 
            GameManager.Instance.TurnPlayer();
            return; 
        }

        GameManager.Instance.TurnEnemy();
    }

}

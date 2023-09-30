using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableGame : MonoBehaviour
{
    //Singleton
    public static TableGame Instance;

    //Values
    public GameObject playerStartRed;
    public GameObject playerStartGreen;
    public GameObject playerStartBlue;

    //StartAI
    public GameObject startAI1;
    public GameObject startAI2;
    public GameObject startAI3;

    //PlayerPoint
    public GameObject playerPoint1;
    public GameObject playerPoint2;
    public GameObject playerPoint3;
    public GameObject playerPoint4;

    //AIPoint
    public GameObject AIPoint1;
    public GameObject AIPoint2;
    public GameObject AIPoint3;
    public GameObject AIPoint4;

    //Point
    public GameObject point5;
    public GameObject point6;
    public GameObject point7;
    public GameObject point8;
    public GameObject point9;
    public GameObject point10;
    public GameObject point11;
    public GameObject point12;
    public GameObject point13;
    public GameObject point14;
    public GameObject point15;
    public GameObject point16;


    private void Awake() 
    {
        Instance = this;
    }

    public GameObject ResetPiecePosition(int numberPiece, bool isPlayer)
    {
        //Reset when Enemy Attack, for this is reason is turn
        if(!isPlayer)
        {
            if(numberPiece == 1)
            {
                return playerStartBlue;
            }
            else if(numberPiece == 2) 
            {
                return playerStartGreen;
            }
            else if(numberPiece == 3)
            {
                return playerStartRed;
            }
        } else 
        {
            if(numberPiece == 1)
            {
                return startAI1;
            }
            else if(numberPiece == 2) 
            {
                return startAI2;
            }
            else if(numberPiece == 3)
            {
                return startAI3;
            }
        }

        return null;
    }

    public GameObject GetPointMove(bool isPlayer, int numberPosition)
    {
        if (numberPosition == 1)
        {
            if(isPlayer)
            {
                return playerPoint1;
            }
            else
            {
                return AIPoint1;
            }
        }
        else if (numberPosition == 2)
        {
            if(isPlayer)
            {
                return playerPoint2;
            }
            else
            {
                return AIPoint2;
            }
        }
        else if (numberPosition == 3)
        {
            if(isPlayer)
            {
                return playerPoint3;
            }
            else
            {
                return AIPoint3;
            }
        }
        else if (numberPosition == 4)
        {
            if(isPlayer)
            {
                return playerPoint4;
            }
            else
            {
                return AIPoint4;
            }
        }
        else if(numberPosition == 5)
        {
            return point5;
        } 
        else if (numberPosition == 6)
        {
            return point6;
        }
        else if (numberPosition == 7)
        {
            return point7;
        }
        else if (numberPosition == 8)
        {
            return point8;
        }
        else if (numberPosition == 9)
        {
            return point9;
        }
        else if (numberPosition == 10)
        {
            return point10;
        }
        else if (numberPosition == 11)
        {
            return point11;
        }
        else if (numberPosition == 12)
        {
            return point12;
        }
        else if (numberPosition == 13)
        {
            return point13;
        }
        else if (numberPosition == 14)
        {
            return point14;
        }
        else if (numberPosition == 15)
        {
            return point15;
        }
        else if (numberPosition == 16)
        {
            return point16;
        }
        return null;
    }
}

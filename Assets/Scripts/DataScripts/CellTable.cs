using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTable : MonoBehaviour
{
    public bool isEmpty;
    public bool isSafeCell;
    public bool occupiedByPlayer;
    public bool occupiedByIA;
    public bool isLastCell;
    public GameObject pieceInTheCell;
    public string nameCell;

    private void Awake() 
    {
        isEmpty = true;
        occupiedByPlayer = false;
        occupiedByIA = false;
        isLastCell = false;
        pieceInTheCell = null;
        nameCell = gameObject.name;

        if(nameCell == "Player_Point_4" || nameCell == "AI_Point_4" || nameCell == "Point_8" || nameCell == "Point_12")
        {
            isSafeCell = true;
        }
        else
        {
            isSafeCell = false;
        }

        if(nameCell == "Point_16")
        {
            isLastCell = true;
        }

    }

}

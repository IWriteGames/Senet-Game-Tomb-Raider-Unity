using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager Instance;

    //Values
    public bool turnPlayer;
    public bool canRoll = false;
    public bool canMoveButtons = false;
    public int numberResult;
    public int pieceFinishedPlayer;
    public int pieceFinishedAI;

    //Piece States
    public bool pieceBlueActive = true;
    public bool pieceGreenActive = true;
    public bool pieceRedActive = true;

    public bool piece1AIActive = true;
    public bool piece2AIActive = true;
    public bool piece3AIActive = true;



    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        if(Random.Range(0, 1) == 0)
        {
            turnPlayer = true;
            canRoll = true;
        } else {
            turnPlayer = false;
        }
    }



    


}

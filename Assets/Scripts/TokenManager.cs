using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    //Singleton
    public static TokenManager Instance;

    [SerializeField] private GameObject piece1;
    [SerializeField] private GameObject piece2;
    [SerializeField] private GameObject piece3;
    [SerializeField] private GameObject piece4;

    private void Awake() 
    {
        Instance = this;
    }

    public int MoveTokens(bool isPlayer)
    {
        int numberResult = 0;
        piece1.transform.rotation = Quaternion.Euler(0, 90, 0);
        piece2.transform.rotation = Quaternion.Euler(0, 90, 0);
        piece3.transform.rotation = Quaternion.Euler(0, 90, 0);
        piece4.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        //Piece1
        int numberPiece1 = Random.Range(2, 9);

        if(numberPiece1 % 2 == 0)
        {
            numberResult++;
        }

        float degreeRotation1 = numberPiece1 * 180f;
        piece1.transform.Rotate(0.0f, degreeRotation1, 0.0f);


        //Piece2
        int numberPiece2 = Random.Range(12, 19);

        if(numberPiece2 % 2 == 0)
        {
            numberResult++;
        }

        float degreeRotation2 = numberPiece2 * 180f;
        piece2.transform.Rotate(0.0f, degreeRotation2, 0.0f);

        //Piece3
        int numberPiece3 = Random.Range(22, 29);

        if(numberPiece3 % 2 == 0)
        {
            numberResult++;
        }

        float degreeRotation3 = numberPiece3 * 180f;
        piece3.transform.Rotate(0.0f, degreeRotation3, 0.0f);

        //Piece4
        int numberPiece4 = Random.Range(32, 39);

        if(numberPiece4 % 2 == 0)
        {
            numberResult++;
        }

        float degreeRotation4 = numberPiece4 * 180f;
        piece4.transform.Rotate(0.0f, degreeRotation4, 0.0f);

        if(isPlayer)
        {
            GameManager.Instance.canRoll = false;
            GameManager.Instance.canMoveButtons = true;
        }

        return numberResult;
    }

}

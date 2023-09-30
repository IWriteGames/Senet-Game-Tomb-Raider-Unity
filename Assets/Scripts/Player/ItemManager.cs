using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IInteractable
{
    [SerializeField] private string nameItem;
    [SerializeField] private int id_type;
    [SerializeField] private Animator anim;


    public string GetInteractPrompt()
    {
        return string.Format(nameItem);
    }

    public int GetIdInteract()
    {
        return id_type;
    }

    public void OnInteract()
    {
        switch (id_type)
        {
            case 1:
                if(!GameManager.Instance.canMoveButtons)
                {
                    return;
                }
                MovePieces.Instance.MovePiece(1);
                anim.SetTrigger("isActivated");
                break;
            case 2:
                if(!GameManager.Instance.canMoveButtons)
                {
                    return;
                }
                MovePieces.Instance.MovePiece(2);
                anim.SetTrigger("isActivated");
                break;
            case 3:
                if(!GameManager.Instance.canMoveButtons)
                {
                    return;
                }
                MovePieces.Instance.MovePiece(3);
                anim.SetTrigger("isActivated");
                break;
            case 4:
                if(!GameManager.Instance.canRoll)
                {
                    return;
                }
                GameManager.Instance.numberResult = TokenManager.Instance.MoveTokens(true);
                // Debug.Log("Player Turn");
                // Debug.Log(GameManager.Instance.numberResult);
                break;
        }
    }

}

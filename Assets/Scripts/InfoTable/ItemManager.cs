using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemMessage;
    [SerializeField] private int idType;
    [SerializeField] private Animator anim;


    public string GetInteractPrompt()
    {
        return string.Format(itemMessage);
    }

    public int GetIdInteract()
    {
        return idType;
    }

    public void OnInteract()
    {
        switch (idType)
        {
            case 1:
            case 2:
            case 3:
                if(!GameManager.Instance.canMoveButtons)
                {
                    return;
                }
                MovementPieces.Instance.MovePiece(idType, true);
                anim.SetTrigger("isActivated");
                break;
            case 4:
                if(!GameManager.Instance.canRoll)
                {
                    return;
                }
                TokenManager.Instance.MoveTokens(true);
                CheckerPieces.Instance.AllPossibilitiesPlayer();
                break;
        }
    }

}

public interface IInteractable
{
    string GetInteractPrompt();
    int GetIdInteract();
    void OnInteract();
}
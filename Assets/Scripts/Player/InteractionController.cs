using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    public float checkrate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject currentInteractGameObject;
    private IInteractable currentInteractable;

    public TextMeshProUGUI promptText;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Time.time -lastCheckTime > checkrate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if(hit.collider.gameObject != currentInteractGameObject)
                {
                    currentInteractGameObject = hit.collider.gameObject;
                    currentInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                currentInteractGameObject = null;
                currentInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void SetPromptText()
    {
        if(GameManager.Instance.turnPlayer)
        {
            if(currentInteractable.GetIdInteract() == 4 && GameManager.Instance.canRoll == false)
            {
                return;
            }
            if(currentInteractable.GetIdInteract() != 4 && GameManager.Instance.canMoveButtons == false)
            {
                return;
            }
            promptText.gameObject.SetActive(true);
            promptText.text = string.Format("<b>[LB]</b> {0}", currentInteractable.GetInteractPrompt());
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(!PlayerController.Instance.gameFinished)
        {
            if(GameManager.Instance.turnPlayer)
            {
                if(!PlayerController.Instance.isPaused)
                {
                    if(context.phase == InputActionPhase.Started && currentInteractable != null)
                    {
                        currentInteractable.OnInteract();
                        currentInteractGameObject = null;
                        currentInteractable = null;
                        promptText.gameObject.SetActive(false);
                    }
                }
            }
        }

    }
}

public interface IInteractable
{
    string GetInteractPrompt();
    int GetIdInteract();
    void OnInteract();
}
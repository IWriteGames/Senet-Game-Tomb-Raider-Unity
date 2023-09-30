using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 currentMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private Rigidbody rig;

    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TextMeshProUGUI finishText;

    public bool isPaused;
    public bool gameFinished;

    private void Awake() 
    {
        Instance = this;
        rig = GetComponent<Rigidbody>();
        pauseMenu.SetActive(false);
        rulesPanel.SetActive(true);
        finishPanel.SetActive(false);
        gameFinished = false;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    private void LateUpdate() 
    {
        if(!gameFinished)
        {
            if(!isPaused)
            {
                CameraLook();
            }
        }
    }
    
    private void FixedUpdate() 
    {
        if(!gameFinished)
        {
            if(!isPaused)
            {
                Move();
            }
        }

    }

    void Move()
    {
        Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        dir *= moveSpeed;

        dir.y = rig.velocity.y;

        rig.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;

        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if(!gameFinished)
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        if(isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            rulesPanel.SetActive(false);
            isPaused = false;
        } else {
            isPaused = true;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void openRules()
    {
        pauseMenu.SetActive(false);
        rulesPanel.SetActive(true);
    }

    public void openFinishedGame(bool isVictory)
    {
        if(isVictory)
        {
            finishText.text = "Victory";
        } else {
            finishText.text = "Defeat";
        }
        Cursor.lockState = CursorLockMode.None;
        finishPanel.SetActive(true);
        gameFinished = true;

    }

    public void reloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
         Application.Quit();
    }

}

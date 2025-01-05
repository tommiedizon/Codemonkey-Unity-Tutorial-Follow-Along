using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed; // c# event
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        OnInteractAction?.Invoke(this, EventArgs.Empty);

    }

    public Vector2 GetMovementVectorNormalized()
    {

        Vector2 playerInput = playerInputActions.Player.Move.ReadValue<Vector2>();

        playerInput = playerInput.normalized;

        return playerInput;
    }
}

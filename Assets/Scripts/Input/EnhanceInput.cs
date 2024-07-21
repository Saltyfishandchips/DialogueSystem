using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnhancedInput : MonoBehaviour
{
    public static EnhancedInput Instance;
    private GameInput gameInput;

    public event EventHandler OnInteractEvent;

    public event EventHandler OnDialogueInteractEvent;
   
    public enum Binding {
        Move_Left,
        Move_Right,
        Interact
    }
    
    private void Awake() {
        if (Instance != null) {
            throw new Exception("PlayerInput.Instance has existed!");
        }
        Instance = this;

        gameInput = new GameInput();
        gameInput.Enable();

        gameInput.Player.Intertact.performed += InteractButtonPressed;
        gameInput.Player.DialogueInput.performed += DialogueInteractButtonPressed;

    }


    public Vector2 GetInputVectorNormalized() {
        if (DialogueManager.Instance.isDialogueIsContinue) {
            return Vector2.zero;
        }

        //使用增强输入
        Vector2 inputVec = gameInput.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;

        return  inputVec;
    }

    private void InteractButtonPressed(InputAction.CallbackContext callback) {
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void DialogueInteractButtonPressed(InputAction.CallbackContext callback) {
        OnDialogueInteractEvent?.Invoke(this, EventArgs.Empty);
    }
    
}

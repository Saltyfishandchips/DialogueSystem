using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnhancedInput : MonoBehaviour
{
    public static EnhancedInput Instance;
    private GameInput gameInput;

    public event EventHandler OnInteractEvent;
    public event EventHandler OnDialogueInteractEvent;
    public event EventHandler OnExitEvent;

   
    public enum Binding {
        Move_Left,
        Move_Right,
        Interact
    }
    
    private void Awake() {

        if (Instance != null)
		{
			Destroy(this.gameObject); return;
		}
		else
		{
			Instance = this;
		}

        gameInput = new GameInput();
        gameInput.Enable();

        gameInput.Player.Intertact.performed += InteractButtonPressed;
        gameInput.Player.DialogueInput.performed += DialogueInteractButtonPressed;
        gameInput.Player.Exit.performed += ExitButtonPressed;

    }
    private void Start() {
        DialogueManager.Instance.OnStoryStart += DialogueDisableInput;
        DialogueManager.Instance.OnStoryEnd += DialogueEnableInput;

        AnimEventTrigger.OnFirstDialogStart += DialogueDisableInput;
        AnimEventTrigger.OnSkipFirstDialog += DialogueEnableInput;

        DayPassUI.OnDayPassUIStart += DialogueDisableInput;
        DayPassUI.OnDayPassUIEnd += DialogueEnableInput;
        
        // 开场禁止输入，先进动画
        DialogueDisableInput(this, EventArgs.Empty);
    }


    public Vector2 GetInputVectorNormalized() {

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
    
    private void ExitButtonPressed(InputAction.CallbackContext callback) {
        OnExitEvent?.Invoke(this, EventArgs.Empty);
    }

    // 对话开始时需要禁用输入
    private void DialogueDisableInput(object sender, EventArgs eventArgs) {
        gameInput.Player.Move.Disable();
        gameInput.Player.Intertact.Disable();
        gameInput.Player.DialogueInput.Enable();
    }

    // 对话结束时需要重启输入
    private void DialogueEnableInput(object sender, EventArgs eventArgs) {
        gameInput.Player.Move.Enable();
        gameInput.Player.Intertact.Enable();
        gameInput.Player.DialogueInput.Disable();
    }

    private void OnDestroy() {
        DialogueManager.Instance.OnStoryStart -= DialogueDisableInput;
        DialogueManager.Instance.OnStoryEnd -= DialogueEnableInput;

        AnimEventTrigger.OnFirstDialogStart -= DialogueDisableInput;
        AnimEventTrigger.OnSkipFirstDialog -= DialogueEnableInput;

        DayPassUI.OnDayPassUIStart -= DialogueDisableInput;
        DayPassUI.OnDayPassUIEnd -= DialogueEnableInput;
    }
}

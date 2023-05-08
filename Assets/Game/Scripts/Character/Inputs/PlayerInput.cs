using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : UnitInput
{
    public Action OnPickUpButtonPressed;

    private void OnEnable() {
        InputManager.Input.Character.Movement.performed += ReadMovementInput;
        InputManager.Input.Character.Movement.canceled += ReadMovementInput;
        InputManager.Input.Character.Sprint.performed += ReadSprintInput;
        InputManager.Input.Character.Sprint.canceled += ReadSprintInput;
        InputManager.Input.Mouse.Semi.performed += ReadShootInput;
        InputManager.Input.Mouse.Automatic.performed += ReadShootInput;
        InputManager.Input.Mouse.MousePosition.performed += ReadLookAtInput;
        InputManager.Input.Character.PickUpGun.performed += ReadSwampGunInput;
    }

    private void OnDisable() {
        InputManager.Input.Character.Movement.performed -= ReadMovementInput;
        InputManager.Input.Character.Movement.canceled -= ReadMovementInput;
        InputManager.Input.Character.Sprint.performed -= ReadSprintInput;
        InputManager.Input.Character.Sprint.canceled -= ReadSprintInput;
        InputManager.Input.Mouse.Semi.performed -= ReadShootInput;
        InputManager.Input.Mouse.Automatic.performed -= ReadShootInput;
        InputManager.Input.Mouse.MousePosition.performed -= ReadLookAtInput;
        InputManager.Input.Character.PickUpGun.performed -= ReadSwampGunInput;
    }
    
    private void ReadMovementInput(InputAction.CallbackContext inputRead) {
        Input = inputRead.ReadValue<Vector2>();
    }
    
    private void ReadLookAtInput(InputAction.CallbackContext inputRead) {
        LookAtInput = inputRead.ReadValue<Vector2>();
    }
    private void ReadSprintInput(InputAction.CallbackContext obj) {
        Sprint = obj.ReadValue<float>() == 1;
    }
    
    private void ReadShootInput(InputAction.CallbackContext inputRead) {
        OnInvokeShootInput?.Invoke(LookAtInput);
    }

    private void ReadSwampGunInput(InputAction.CallbackContext inputRead) {
        OnPickUpButtonPressed?.Invoke();
    }
}

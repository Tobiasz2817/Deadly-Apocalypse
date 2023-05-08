
using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : Shooting
{
    private Coroutine shootCoroutine;
    
    private void Start() {
        DisableShootInputs();
    }

    public override void SetGunReference(Gun gun_) {
        base.SetGunReference(gun_);
        EnableInputsBasedOnTypeFire(gun_.shootType);
    }

    public override void Shoot(Vector2 mousePosition) {
        if(gun) gun.TryFire(mousePosition);
    }

    #region ModifyPCInputs

    private void EnableInputsBasedOnTypeFire(ShootType typeShooting) {
        switch (typeShooting)
        {
            case ShootType.Automatic:
            {
                InputManager.Input.Mouse.Automatic.Enable();
                InputManager.Input.Mouse.Semi.Disable();
            }
                break;
            case ShootType.Semi:
            {
                InputManager.Input.Mouse.Automatic.Disable();
                InputManager.Input.Mouse.Semi.Enable();
            }
                break;
        }
    }

    private void DisableShootInputs() {
        InputManager.Input.Mouse.Automatic.Disable();
        InputManager.Input.Mouse.Semi.Disable();
    }

    #endregion
}

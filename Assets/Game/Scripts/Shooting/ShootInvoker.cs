using System;
using UnityEngine;

public class ShootInvoker : MonoBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private UnitInput unitInput;
    
    private void OnEnable() {
        unitInput.OnInvokeShootInput += shooting.Shoot;
    }
    
    private void OnDisable() {
        unitInput.OnInvokeShootInput -= shooting.Shoot;
    }
}

using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ShootInvoker : NetworkBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private UnitInput unitInput;

    private ShootDependencies shootDependencies;
    private void OnEnable() {
        unitInput.OnInvokeShootInput += Shooting;
    }

    private void OnDisable() {
        unitInput.OnInvokeShootInput -= Shooting;
    }

    private void Shooting(Vector2 obj) {
        shootDependencies.ownerShootKey = NetworkPoller.Instance.OwnerBy;
        shootDependencies.direction = obj;
        ShootServerRpc(shootDependencies);
        Shoot(shootDependencies);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void ShootServerRpc(ShootDependencies shootDependencies_) {
        ShootClientRpc(shootDependencies_);
    }
    
    [ClientRpc]
    private void ShootClientRpc(ShootDependencies shootDependencies_) {
        if (IsOwner) return;
        Shoot(shootDependencies_);
    }
    
    private void Shoot(ShootDependencies shootDependencies_) {
        shooting.Shoot(shootDependencies_);
    }
}

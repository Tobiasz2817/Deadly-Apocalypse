using Unity.Netcode;
using UnityEngine;

public abstract class Shooting : NetworkBehaviour
{
    [SerializeField] protected Gun gun;

    public virtual void SetGunReference(Gun gun_) { this.gun = gun_; }
    public abstract void Shoot(ShootDependencies shootDependencies);
}


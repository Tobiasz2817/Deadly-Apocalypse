using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    [SerializeField] protected Gun gun;

    public virtual void SetGunReference(Gun gun_) { this.gun = gun_; }
    public abstract void Shoot(Vector2 mousePosition);
}


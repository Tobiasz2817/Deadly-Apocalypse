using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : PolledObject
{
    [SerializeField] protected float speedBullet;
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Transform lookAtPoint;
    
    [SerializeField] protected float shootDelay;
    [SerializeField] public ShootType shootType;

    [SerializeField] private BoxCollider2D gunCollider;

    private bool canShoot = true;
    public bool CanShoot { private set => canShoot = value; get => canShoot; }
    
    public void TryFire(Vector2 mousePosition) {
        if (!canShoot) return;
        
        StartCoroutine(ShootDelay());
        Fire(RevertVectorOnWorldPoint(mousePosition));
    }

    private Vector2 RevertVectorOnWorldPoint(Vector2 mousePosition) {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    
    private IEnumerator ShootDelay() {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
    
    protected abstract void Fire(Vector2 direction);

    public Transform GetShootPoint() {
        return shootPoint;
    }
    public Transform GetLookAtPoint() {
        return lookAtPoint;
    }
    public void ChangeStateCollider(bool isActive) {
        gunCollider.enabled = isActive;
    }
}
public enum ShootType
{
    Automatic,
    Semi
}
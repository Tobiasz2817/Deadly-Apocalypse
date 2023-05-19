using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Gun : PolledObject
{
    [SerializeField] protected float speedBullet;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Transform lookAtPoint;
    
    [SerializeField] protected float shootDelay;
    [SerializeField] protected float damage;
    [SerializeField] public ShootType shootType;

    [SerializeField] private Bullet bullet;
    [SerializeField] private BoxCollider2D gunCollider;

    private bool canShoot = true;
    public bool CanShoot { private set => canShoot = value; get => canShoot; }
    
    public void TryFire(ShootDependencies shootDependencies) {
        if (!canShoot) return;
        
        StartCoroutine(ShootDelay());
        ApplyGunShootDependencies(ref shootDependencies);
        Fire(shootDependencies);
    }

    private void ApplyGunShootDependencies(ref ShootDependencies shootDependencies) {
        shootDependencies.direction = RevertVectorOnWorldPoint(shootDependencies.direction);
        shootDependencies.damage = damage;
        shootDependencies.shootPoint = shootPoint.position;
    }
    
    private Vector2 RevertVectorOnWorldPoint(Vector2 mousePosition) {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    
    private IEnumerator ShootDelay() {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
    
    protected abstract void Fire(ShootDependencies shootDependencies);

    public Transform GetShootPoint() {
        return shootPoint;
    }
    public Transform GetLookAtPoint() {
        return lookAtPoint;
    }
    public void ChangeStateCollider(bool isActive) {
        gunCollider.enabled = isActive;
    }

    public void ReversBullets(Owner ownerKey) {
        if (NetworkPoller.Instance == null) return;
        
        NetworkPoller.Instance.ReversObjects(ownerKey,ObjectPollTypes.GunBullets,bullet.GetType());
    }
}

public struct ShootDependencies : INetworkSerializable
{
    public Vector2 direction;
    public Vector2 shootPoint;
    public Owner ownerShootKey;
    public float damage;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        serializer.SerializeValue(ref direction);
        serializer.SerializeValue(ref shootPoint);
        serializer.SerializeValue(ref ownerShootKey);
        serializer.SerializeValue(ref damage);
    }
}

public enum ShootType
{
    Automatic,
    Semi
}
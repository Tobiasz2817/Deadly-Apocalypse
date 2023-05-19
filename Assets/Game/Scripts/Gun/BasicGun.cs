
using UnityEngine;

public class BasicGun : Gun
{
    protected override void Fire(ShootDependencies shootDependencies) {
        var bullet =  (Bullet)NetworkPoller.Instance.GetObject(shootDependencies.ownerShootKey,ObjectPollTypes.GunBullets);
        bullet.SetShootDependencies(shootDependencies);
        bullet.RotateTo();
        bullet.transform.position = shootDependencies.shootPoint;
        bullet.gameObject.SetActive(true);
        bullet.MoveTowards((shootDependencies.direction - (Vector2)shootPoint.position).normalized);
    }
}

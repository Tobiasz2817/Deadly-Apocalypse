using UnityEngine;

public class FrozenGun : Gun
{
    protected override void Fire(Vector2 direction) {
        var dir = -shootPoint.right;
        
        var bullet =  (Bullet)SinglePoller.Instance.GetObject(OwnerType,ObjectPollTypes.GunBullets);
        bullet.RotateTo(direction);
        bullet.transform.position = shootPoint.position;
        bullet.gameObject.SetActive(true);
        bullet.MoveTowards((direction - (Vector2)shootPoint.position).normalized);
    }
}

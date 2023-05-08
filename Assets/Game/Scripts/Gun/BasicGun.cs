
using UnityEngine;

public class BasicGun : Gun
{
    protected override void Fire(Vector2 direction) {
        var dir = -shootPoint.right;
        
        var bullet =  (Bullet)SinglePoller.Instance.GetObject(OwnerType,ObjectPollTypes.GunBullets);
        bullet.RotateTo(direction);
        bullet.transform.position = shootPoint.position;
        bullet.gameObject.SetActive(true);
        bullet.MoveTowards(dir);
    }
}

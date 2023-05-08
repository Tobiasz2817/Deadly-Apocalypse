using UnityEngine;

public class FrozenBullet : Bullet
{
    public float speedBullet = 120f;

    public override void MoveTo(Vector2 direction) {
        transform.Translate(direction * Time.deltaTime * speedBullet);
    }
}

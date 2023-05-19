using UnityEngine;

public class FrozenBullet : Bullet
{
    public float speedBullet = 120f;

    protected override void MoveTo(Vector2 direction) {
        transform.Translate(direction * Time.deltaTime * speedBullet);
    }
}

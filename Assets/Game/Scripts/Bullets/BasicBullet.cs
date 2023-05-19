
using UnityEngine;

public class BasicBullet : Bullet
{
    public float speedBullet = 120f;

    protected override void MoveTo(Vector2 direction) {
        transform.Translate(direction * Time.deltaTime * speedBullet);
    }
}
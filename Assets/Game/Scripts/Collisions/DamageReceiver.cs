using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : NetworkBehaviour
{
    public UnityEvent<float> OnGetHit;
    private void OnTriggerEnter2D(Collider2D other) {
        if (!IsOwner) return;

        var bullet = other.GetComponentInParent<Bullet>();
        if (bullet) {
            OnGetHit?.Invoke(bullet.GetShootDependencies.damage);
        }
    }
}

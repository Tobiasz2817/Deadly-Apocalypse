using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected Transform target;

    public abstract Vector2 MoveTo(Vector2 direction);
    public abstract void ModificateSpeed(float speedValue);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDamp : Movement
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float maxSpeed = 15f;

    public override Vector2 MoveTo(Vector2 direction) {
        Vector2 damp = Vector2.SmoothDamp(target.position, direction, ref velocity, smoothTime, maxSpeed, Time.deltaTime);
        target.position = damp;
        return damp;
    }

    public override void ModificateSpeed(float speedValue) {
        smoothTime = speedValue;
    }
}

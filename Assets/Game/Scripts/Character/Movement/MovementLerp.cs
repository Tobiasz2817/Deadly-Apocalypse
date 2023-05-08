using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLerp : Movement
{
    public float lerpT = 12f;
    
    public override Vector2 MoveTo(Vector2 direction) {
        Vector2 lerp = Vector2.Lerp(target.position, direction, lerpT * Time.deltaTime);
        target.position = lerp;
        return lerp;
    }

    public override void ModificateSpeed(float speedValue) {
        lerpT = speedValue;
    }
}

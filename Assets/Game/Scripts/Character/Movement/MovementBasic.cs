using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasic : Movement
{
    public float movementSpeed;
    
    public override Vector2 MoveTo(Vector2 direction) {
        Vector2 dir = direction * Time.deltaTime * movementSpeed; 
        target.Translate(dir);
        return dir;
    }

    public override void ModificateSpeed(float speedValue) {
        movementSpeed = speedValue;
    }
}
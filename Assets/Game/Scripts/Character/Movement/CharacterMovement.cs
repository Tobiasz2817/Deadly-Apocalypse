using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private UnitInput unitInput;
    [SerializeField] private Movement movement;
    
    private bool sprint;

    private void Start() {
        Sprint(false);
    }

    private void OnEnable() {
        unitInput.OnSprint += Sprint;
    }
    private void OnDisable() {
        unitInput.OnSprint -= Sprint;
    }
    private void Update() {
        if (unitInput.Input == Vector2.zero) return;
        MoveTo(unitInput.Input);
    }

    private void MoveTo(Vector2 direction) {
        movement.MoveTo((Vector2)transform.position + direction);
    }

    private void Sprint(bool isSprint) {
        if(isSprint)
            movement.ModificateSpeed(0.1f);
        else 
            movement.ModificateSpeed(0.2f);
    }
}

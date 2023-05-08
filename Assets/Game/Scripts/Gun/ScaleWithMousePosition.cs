using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleWithMousePosition : MonoBehaviour
{
    [SerializeField] private Transform targetScale;
    [SerializeField] private UnitInput unitInput;
    
    private Vector2 mouseInput;

    [SerializeField] private bool modifyScaleX;
    [SerializeField] private float leftSideX;
    [SerializeField] private float rightSideX;

    [SerializeField] private bool modifyScaleY;
    [SerializeField] private float leftSideY;
    [SerializeField] private float rightSideY;
    
    private void OnEnable() {
        unitInput.OnChangeInputLookAt += ReadInputs;
    }
    private void OnDisable() {
        unitInput.OnChangeInputLookAt -= ReadInputs;
    }
    
    private void ReadInputs(Vector2 input) {
        if (input == Vector2.zero) return;
        mouseInput = input;
    }

    private void Update() {
        ChangeScale();
    }

    private void ChangeScale() {
        bool isMouseRightOfPlayer = mouseInput.x >= 0;

        var leftSideX_ = modifyScaleX ? leftSideX : targetScale.localScale.x;
        var rightSideX_ = modifyScaleX ? rightSideX : targetScale.localScale.x;
        
        var leftSideY_ = modifyScaleY ? leftSideY : targetScale.localScale.y;
        var rightSideY_ = modifyScaleY ? rightSideY : targetScale.localScale.y;
        
        if (isMouseRightOfPlayer) targetScale.localScale = new Vector3(rightSideX_, rightSideY_);
        else targetScale.localScale = new Vector3(leftSideX_, leftSideY_);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOffsetChanger : MonoBehaviour
{
    [SerializeField] private RotateWithCursor rotateWithCursor;

    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;

    private void Update() {
        ChangeOffSet(rotateWithCursor,transform);
    }

    private void ChangeOffSet(RotateWithCursor rotateWithCursor, Transform gunRefenrece) {
        if(gunRefenrece.localScale.x == 1)
            rotateWithCursor.SetOffset(leftOffset);
        else 
            rotateWithCursor.SetOffset(rightOffset);
    }
}

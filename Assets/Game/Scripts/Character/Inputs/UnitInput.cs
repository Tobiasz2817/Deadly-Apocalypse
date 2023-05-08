using System;
using UnityEngine;

public class UnitInput : MonoBehaviour
{
    private Vector2 input;
    public Vector2 Input
    {
        protected set {
            input = value;
            OnChangeInputMovement?.Invoke(input);
        }
        get => input;
    }

    public event Action<Vector2> OnChangeInputMovement;
    
    private Vector2 lookAtInput;
    public Vector2 LookAtInput
    {
        protected set {
            lookAtInput = value;
            OnChangeInputLookAt?.Invoke(lookAtInput);
        }
        get => lookAtInput;
    }

    public event Action<Vector2> OnChangeInputLookAt;


    private bool sprint;
    
    public bool Sprint
    {
        protected set {
            sprint = value;
            OnSprint?.Invoke(sprint);
        }
        get => sprint;
    }

    public event Action<bool> OnSprint;
    public Action<Vector2> OnInvokeShootInput;
}


using System;
using UnityEngine;

public abstract class PolledObject : MonoBehaviour
{
    public int objectId;
    [SerializeField] public Rigidbody rb;
    
    [field: SerializeField] public ObjectPollTypes TypeVisibler;
    private ObjectPollTypes type;
    public ObjectPollTypes Type {
        set {
            if (typeWasModifide) return;
            type = value;
            TypeVisibler = value;
            typeWasModifide = true;
        }
        get => type;
    }
    private bool typeWasModifide = false;
    
    [field: SerializeField] public Owner ownerTypeVisibler;
    private Owner ownerType;
    
    public Owner OwnerType {
        set {
            if (ownerTypeWasModifide) return;
            ownerType = value;
            ownerTypeVisibler = value;
            ownerTypeWasModifide = true;
        }
        get => ownerType;
    }
    private bool ownerTypeWasModifide = false; 
}

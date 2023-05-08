using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PollerData : MonoBehaviour
{
    public int count = 1;
    public bool InitAtStart = true; 
    public ObjectPollTypes objectPollTypes;
    public Owner owner;
    public abstract PolledObject GetPolledObject(int index);
    public abstract List<PolledObject> CreatePolledObjects(Transform transform);
    public abstract int GetIndexObjectsByType(Type type);
    public abstract int GetPrefabsCount();
}

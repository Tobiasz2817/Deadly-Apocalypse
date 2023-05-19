using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(PollerData))]
public class NetworkPoller : SingletonNetwork<NetworkPoller>
{
    [SerializeField] 
    private Dictionary<Owner,Dictionary<ObjectPollTypes,PollerPlaceHolder>> objectsToPoll = new Dictionary<Owner, Dictionary<ObjectPollTypes, PollerPlaceHolder>>();
    private Dictionary<Owner, Transform> placeHolders = new Dictionary<Owner, Transform>();

    //public event Action OnPollerInstances; 

    [field: SerializeField] public Owner OwnerBy { private set; get; }
    public bool PollerCreated { private set => pollerCreated = value;  get => pollerCreated; }
    public bool pollerCreated = false;

    public override void Awake() {
        base.Awake();
        InitPoller();
        PollerCreated = true;
    }


    #region Init Poller

    public void InitPoller() {
        var pollersData = GetComponents<PollerData>();
        foreach (var pollerData in pollersData) {
            if (!pollerData.InitAtStart) continue;

            CreatePollObjects(pollerData,null);
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void InitPollerFromExistPollerDataServerRpc(NetworkObjectReference owner,ulong myKey,Owner ownerKey,params Owner[] setPossiblyKeyFrom) {
        InitPollerFromExistPollerDataClientRpc(owner,myKey,ownerKey,setPossiblyKeyFrom);
    }
    
    [ClientRpc]
    public void InitPollerFromExistPollerDataClientRpc(NetworkObjectReference owner,ulong myKey,Owner ownerKey,params Owner[] setPossiblyKeyFrom) {
        InitPollerFromExistPollerData(owner,myKey,ownerKey,setPossiblyKeyFrom);
    }
    
    public Owner InitPollerFromExistPollerData(NetworkObjectReference owner,ulong myKey,Owner ownerKey,params Owner[] setPossiblyKeyFrom) {
        var pollersData = GetComponents<PollerData>();
        var newKey = GetOwnerByPlayerKey(myKey,setPossiblyKeyFrom);
        if (newKey == Owner.Null) return newKey;
        if (objectsToPoll.ContainsKey(newKey)) return newKey;
        
        foreach (var pollerData in pollersData) {
            if (ownerKey != pollerData.owner) continue;

            pollerData.owner = newKey;
            if (owner.TryGet(out NetworkObject networkObject)) {
                CreatePollObjects(pollerData,networkObject.transform);
                if (networkObject.IsOwner)
                    OwnerBy = newKey;
            }
            pollerData.owner = ownerKey;
        }
        
        return newKey;
    }
    
    // Safe Network Feature
    public Owner GetOwnerByPlayerKey(ulong myKey,Owner[] keys) {
        for (int i = 0; i < keys.Length; i++) 
            if (myKey == (ulong)i)
                return keys[i];

        return Owner.Null;
    }

    #region Extension Init

    public Owner InitPollerFromExistPollerData(Transform owner, ObjectPollTypes objectPollTypes,Owner ownerKey,params Owner[] setPossiblyKeyFrom) {
        var pollersData = GetComponents<PollerData>();
        var newKey = GetOwnerByEmptyPlace(setPossiblyKeyFrom);
        if (newKey == Owner.Null) return newKey;
        
        foreach (var pollerData in pollersData) {
            if (ownerKey != pollerData.owner) continue; 
            if (objectPollTypes != pollerData.objectPollTypes) continue;
            
            pollerData.owner = newKey;
            CreatePollObjects(pollerData,owner);
            pollerData.owner = ownerKey;

            return newKey;
        }

        return newKey;
    }
    
    public Owner InitPollerFromExistPollerData(Transform owner,Owner ownerKey,params Owner[] setPossiblyKeyFrom) {
        var pollersData = GetComponents<PollerData>();
        var newKey = GetOwnerByEmptyPlace(setPossiblyKeyFrom);
        if (newKey == Owner.Null) return newKey;
        
        foreach (var pollerData in pollersData) {
            if (ownerKey != pollerData.owner) continue;

            pollerData.owner = newKey;
            CreatePollObjects(pollerData,owner);
        }

        return newKey;
    }
    
    private Owner GetOwnerByEmptyPlace(Owner[] keys) {
        foreach (var key in keys) {
            if (!objectsToPoll.ContainsKey(key))
                return key;
        }

        return Owner.Null;
    }
    
    [ClientRpc]
    public void RemoveKeyClientRpc(Owner owner, float timeToRemove) {
        RemoveKey(owner, timeToRemove);
    }
    
    public void RemoveKey(Owner owner, float timeToRemove) {
        StartCoroutine(RemoveAfterTime(owner,timeToRemove));
    }

    private IEnumerator RemoveAfterTime(Owner owner, float timeToRemove) {
        yield return new WaitForSeconds(timeToRemove);
        foreach (var values in objectsToPoll[owner]) 
            values.Value.DestroyObjects();
        

        Destroy(placeHolders[owner].gameObject);
        placeHolders.Remove(owner);
    }

    #endregion
    

    
    private void CreatePollObjects(PollerData pollerData, Transform owner) {
        if (objectsToPoll.TryGetValue(pollerData.owner, out var values)) 
            if (values.ContainsKey(pollerData.objectPollTypes))
                return;
        
        // Creating Owner GameObject
        if(!placeHolders.ContainsKey(pollerData.owner)) {
            GameObject placeHolderOwner = new GameObject(pollerData.owner.ToString());
            placeHolderOwner.transform.SetParent(transform);
            placeHolders.Add(pollerData.owner,placeHolderOwner.transform);
        }
            
        // Creating Placeholder for PollTypes
        GameObject placeHolder = new GameObject(pollerData.objectPollTypes.ToString());
        placeHolder.transform.SetParent(placeHolders[pollerData.owner]);
            
        // Init Dates
        var pollerPlaceHolder = placeHolder.AddComponent<PollerPlaceHolder>();
        pollerPlaceHolder.InitPoller(pollerData);
        pollerPlaceHolder.InitTransform(owner);

        if (values != null) {
            values.Add(pollerData.objectPollTypes,pollerPlaceHolder);
        }
        else {
            var newDict = new Dictionary<ObjectPollTypes, PollerPlaceHolder>();
            newDict.Add(pollerData.objectPollTypes,pollerPlaceHolder);
            objectsToPoll.Add(pollerData.owner,newDict);
        }
    }
    
    
    #endregion
    


    #region Callback PolledObjects
    
    public PolledObject GetObject (Owner ownerType, ObjectPollTypes typeObject,bool state) {
        var polledObject = objectsToPoll[ownerType][typeObject].GetUnActiveObject();
        return polledObject;
    }
    
    

    public PolledObject[] GetObjects (Owner ownerType, ObjectPollTypes typeObject,int objectsCount) {
        var polledObjects = objectsToPoll[ownerType][typeObject].GetUnActiveObjects(objectsCount);

        return polledObjects;
    }
    public PolledObject[] GetObjects (Owner ownerType, ObjectPollTypes typeObject,Type type, int countObjects) {
        return objectsToPoll[ownerType][typeObject].GetUnActiveObjects(type,countObjects);
    }
    public List<PolledObject> GetListObjects (Owner ownerType, ObjectPollTypes typeObject,Type type, int countObjects) {
        return objectsToPoll[ownerType][typeObject].GetUnActiveListObjects(type,countObjects);
    }
    public PolledObject GetObject (Owner ownerType, ObjectPollTypes typeObject,int index) {
        return objectsToPoll[ownerType][typeObject].GetObject(index);
    }
    public PolledObject GetActiveObject (Owner ownerType, ObjectPollTypes typeObject,int index) {
        return objectsToPoll[ownerType][typeObject].GetActiveObject(index);
    }
    public PolledObject GetObject (Owner ownerType, ObjectPollTypes typeObject) {
        return objectsToPoll[ownerType][typeObject].GetUnActiveObject();
    }

    #endregion

    #region Callback Genercis T
    
    public T GetObject<T> (Owner ownerType, ObjectPollTypes typeObject,int index) where T : PolledObject {
        return (T)objectsToPoll[ownerType][typeObject].GetObject(index);
    }
    
    public T GetObjectByGenericType<T> (Owner ownerType, ObjectPollTypes typeObject) where T : PolledObject {
        return (T)objectsToPoll[ownerType][typeObject].GetUnActiveObject(typeof(T));
    }
    public T[] GetObjectsByGenericType<T> (Owner ownerType, ObjectPollTypes typeObject,int count) where T : PolledObject {
        return (T[])objectsToPoll[ownerType][typeObject].GetUnActiveObjects(typeof(T),count);
    }
    
    public T[] GetActiveObjectsByGenericType<T> (Owner ownerType, ObjectPollTypes typeObject,int count) where T : PolledObject {
        return (T[])objectsToPoll[ownerType][typeObject].GetUnActiveObjects(typeof(T),count);
    }
    
    public T GetObject<T> (Owner ownerType, ObjectPollTypes typeObject) where T : PolledObject {
        return (T)objectsToPoll[ownerType][typeObject].GetUnActiveObject();
    }
    
    #endregion

    #region Callback Index

    public int GetIndexObject<T> (Owner ownerType, ObjectPollTypes typeObject) where T : PolledObject {
        return objectsToPoll[ownerType][typeObject].GetIndexObject(typeof(T));
    }
    
    public int GetIndexPrefab(Owner ownerType, ObjectPollTypes typeObject, Type type) {
        return objectsToPoll[ownerType][typeObject].GetIndexPrefab(type);
    }
    
    public int GetIndexObject (Owner ownerType, ObjectPollTypes typeObject, Type type) {
        return objectsToPoll[ownerType][typeObject].GetIndexObject(type);
    }
    
    public List<int> GetObjectsIndexesDifferentTypes(Owner ownerType, ObjectPollTypes typeObject,int countPerType) {
        return objectsToPoll[ownerType][typeObject].GetObjectsIndexesDifferentTypes(countPerType);
    }
    
        
    public int[] GetIndexes(Owner ownerType, ObjectPollTypes typeObject,int objectsCount) {
        var polledObjects = objectsToPoll[ownerType][typeObject].GetUnActiveIndexes(objectsCount);

        return polledObjects;
    }
    
    public int GetIndex(Owner ownerType, ObjectPollTypes typeObject) {
        var polledObjects = objectsToPoll[ownerType][typeObject].GetUnActiveIndex();

        return polledObjects;
    }
    
    #endregion
    

    #region Add / Remove / Revers Operations

    
    public void ReversObjects(Owner ownerType,ObjectPollTypes pollTypes,int index) {
        if (objectsToPoll.ContainsKey(ownerType)) {
            if (objectsToPoll[ownerType].ContainsKey(pollTypes)) {
                objectsToPoll[ownerType][pollTypes].ReverseOnNewObject(index);
            }
        }
    }
    
    public void ReversObjects(Owner ownerType,ObjectPollTypes pollTypes,Type type) {
        Debug.Log(ownerType + " " + pollTypes + " type: " + type);
        if (objectsToPoll.ContainsKey(ownerType)) {
            if (objectsToPoll[ownerType].ContainsKey(pollTypes)) {
                objectsToPoll[ownerType][pollTypes].ReverseOnNewObject(type);
            }
        }
    }

    #endregion

    public void PollObject(Owner ownerType,ObjectPollTypes pollTypes,int index) {
        objectsToPoll[ownerType][pollTypes].PollActiveObject(index);
    }
    

    
    
  
}
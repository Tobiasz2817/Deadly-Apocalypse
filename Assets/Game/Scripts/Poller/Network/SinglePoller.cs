using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

[RequireComponent(typeof(PollerData))]
public class SinglePoller : Singleton<SinglePoller>
{
    private Dictionary<Owner,Dictionary<ObjectPollTypes,PollerPlaceHolder>>  objectsToPoll = new Dictionary<Owner,Dictionary<ObjectPollTypes,PollerPlaceHolder>>();
    public bool PollerCreated { private set => pollerCreated = value;  get => pollerCreated; }
    public bool pollerCreated = false;

    private void OnEnable() {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1) {
        InitPoller();
        PollerCreated = true;
    }

    private void InitPoller() {
        var types = GetComponents<PollerData>();

        foreach (var pollerData in types) {
            GameObject placeHolder = new GameObject(pollerData.owner + "/" + pollerData.objectPollTypes);
            placeHolder.transform.SetParent(transform);
            var pollerPlaceHolder = placeHolder.AddComponent<PollerPlaceHolder>();
            pollerPlaceHolder.InitPoller(pollerData);
            if (objectsToPoll.TryGetValue(pollerData.owner, out var value)) {
                value.Add(pollerData.objectPollTypes,pollerPlaceHolder);
            }
            else {
                var newDict = new Dictionary<ObjectPollTypes, PollerPlaceHolder>();
                newDict.Add(pollerData.objectPollTypes,pollerPlaceHolder);
                objectsToPoll.Add(pollerData.owner,newDict);
            }
        }
    }


    

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
                Debug.Log("Reversing");
            }
        }
    }

    #endregion

    public void PollObject(Owner ownerType,ObjectPollTypes pollTypes,int index) {
        objectsToPoll[ownerType][pollTypes].PollActiveObject(index);
    }

}

public enum Owner
{
    Null,
    AI,
    Player,
    Player_1,
    Player_2,
    Player_3,
    Player_4,
    Player_5,
    Player_6,
    Player_7,
}
using System;
using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkSpawner : MonoBehaviour
{
    // Only server can invoke
    public void SpawnServerRpc(GameObject characterPrefab,Vector3 position,Quaternion rotation,bool destroyWithScene = false) {
        Spawn(characterPrefab, position, rotation, 0, destroyWithScene,false);
    }
    public void SpawnServerRpc(GameObject characterPrefab,Vector3 position,Quaternion rotation,ulong clientId,bool destroyWithScene = false) {
         Spawn(characterPrefab, position, rotation, clientId, destroyWithScene,true);
    }
    public NetworkObject Spawn(GameObject characterPrefab,Vector3 position,Quaternion rotation,ulong clientId, bool destroyWithScene, bool ownerShipSpawn) {
        var obj_ = Instantiate(characterPrefab,position, rotation);
        
        if (obj_.TryGetComponent(out NetworkObject networkObject)) 
            if(ownerShipSpawn)
                networkObject.SpawnWithOwnership(clientId,destroyWithScene);
            else 
                networkObject.Spawn(destroyWithScene);
        else
            throw new Exception("Prefab must have NetworkObject");
        
        return networkObject;
    }
}

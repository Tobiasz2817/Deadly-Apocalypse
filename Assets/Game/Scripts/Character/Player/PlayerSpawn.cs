using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerSpawn : NetworkBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject prefab;
    [SerializeField] private CharacterNetworkSpawner characterNetworkSpawner;
    
    public UnityEvent<NetworkObject> OnPlayerSpawn;

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnPlayerLoad;
    }
    
    public override void OnNetworkDespawn() {
        if (!IsServer || !NetworkManager.Singleton) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnPlayerLoad;
    }
    
    private void OnPlayerLoad(ulong clientid, string scenename, LoadSceneMode loadscenemode) {
        // SpawnSelf
        SpawnPlayer(clientid);
    }

    private void SpawnPlayer(ulong clientId) {
         var player = characterNetworkSpawner.SpawnPlayer(prefab,spawnPoints[(int)clientId].position,Quaternion.identity,clientId,true);
         OnPlayerSpawn?.Invoke(player);
    }

}

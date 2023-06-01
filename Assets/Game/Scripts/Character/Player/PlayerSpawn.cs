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
    [SerializeField] private bool spawnOnStart = false;
    
    public UnityEvent<NetworkObject> OnPlayerSpawn;
    private Dictionary<ulong, NetworkObject> players = new Dictionary<ulong, NetworkObject>();

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnPlayerLoad;
    }
    

    public override void OnNetworkDespawn() {
        if (!IsServer || !NetworkManager.Singleton) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnPlayerLoad;
    }

    private void OnPlayerLoad(ulong clientid, string scenename, LoadSceneMode loadscenemode) {
        Debug.Log("LOAD");
        if (spawnOnStart)
            SpawnPlayer(clientid);
    }

    /*public void SpawnConnectedPlayers() {
        if (!NetworkManager.Singleton) return;
        foreach (var client in NetworkManager.Singleton.ConnectedClients) {
            if (client.Value.PlayerObject != null) continue;
            Debug.Log("SpawnClient");
            SpawnPlayer(client.Key);
        }   
    }*/

    public void RespawnPlayers() {
        foreach (var player in players) {
            player.Value.transform.position = spawnPoints[(int)player.Key].position;
        }
    }

    private void SpawnPlayer(ulong clientId) {
         var player = characterNetworkSpawner.SpawnPlayer(prefab,spawnPoints[(int)clientId].position,Quaternion.identity,clientId,true);
         players.TryAdd(clientId,player);
         OnPlayerSpawn?.Invoke(player);
    }
}

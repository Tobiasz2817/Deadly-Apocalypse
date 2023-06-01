using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class ControlClientReceivers : NetworkBehaviour
{
    private List<ClientSpawnReceiver> spawnReceivers = new List<ClientSpawnReceiver>();
    private List<ClientDespawnReceiver> deSpawnReceivers = new List<ClientDespawnReceiver>();

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        
        spawnReceivers = FindObjectsOfType<ClientReceiverHandler>()
            .SelectMany(handler => handler.GetComponentsInChildren<ClientSpawnReceiver>())
            .OrderBy(receiver => receiver.priority).ToList();
        deSpawnReceivers = FindObjectsOfType<ClientReceiverHandler>()
            .SelectMany(handler => handler.GetComponentsInChildren<ClientDespawnReceiver>())
            .ToList();

        Debug.Log(spawnReceivers.Count);
    }

    public void InvokeSpawnToReceivers(NetworkObject spawnedClient) {
        foreach (var spawnReceiver in spawnReceivers) {
            spawnReceiver.SpawnedClient(spawnedClient);
        }
    }
    

    public void InvokeDeSpawnToReceivers(NetworkObject deSpawnedClient) {
        foreach (var deSpawnReceiver in deSpawnReceivers) {
            deSpawnReceiver.SpawnedClient(deSpawnedClient);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayersStateController : NetworkBehaviour, ClientSpawnReceiver
{
    private Dictionary<ulong, NetworkObjectReference> players = new Dictionary<ulong, NetworkObjectReference>();

    private void AddPlayer(NetworkObjectReference objectReference) {
        if (objectReference.TryGet(out NetworkObject networkObject)) {
            players.TryAdd(networkObject.OwnerClientId, networkObject);
        }
    }

    public void ChangeClientsState(bool state) {
        Debug.Log("Respawn invoke " + players.Count);
        ChangeClientsStateClientRpc(players.Values.ToArray(),state);
    }

    [ClientRpc]
    public void ChangeClientsStateClientRpc(NetworkObjectReference[] players ,bool state) {
        foreach (var key in players) {
            ChangeState(key,state);
        }
    }

    public void BUTTON(bool state) {
        ChangeClientsState(state);
    }

    public void ChangeState(NetworkObject player, bool state) {
        player.gameObject.SetActive(state);
    }
    
    
    [field:SerializeField] public int priority { get; set; }
    public void SpawnedClient(NetworkObject client) {
        AddPlayer(client);
        Debug.Log("SpawnedClient in PlayersStateController " + players.Count);
    }
}


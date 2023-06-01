using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersReferenceData : NetworkBehaviour
{
    private Dictionary<ulong, NetworkObjectReference> players = new Dictionary<ulong, NetworkObjectReference>();
    
    public override void OnNetworkSpawn() {
        if (!IsServer|| !NetworkManager.Singleton) return;
        NetworkManager.Singleton.OnClientDisconnectCallback += PlayerDisconnect;
    }
    
    public override void OnNetworkDespawn() {
        if (!IsServer || !NetworkManager.Singleton) return;
        NetworkManager.Singleton.OnClientDisconnectCallback -= PlayerDisconnect;
    }

    private void PlayerDisconnect(ulong obj) {
        players.Remove(obj);
    }
    
    public void LoadPlayer(ulong clientId) {
        StartCoroutine(WaitForPlayerReference(clientId));
    }
    
    private IEnumerator WaitForPlayerReference(ulong id) {
        NetworkObject player = null;
        do {
            player = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(id);
            yield return null;
        } while (player != null);

        AddPlayer(player);
    }
    
    public void LoadPlayer(NetworkObjectReference clientRef) {
        AddPlayer(clientRef);
        ConfigurePlayersClientRpc(players.Values.ToArray());
    }
    
    public void LoadPlayer(NetworkObject clientRef) {
        AddPlayer(clientRef);
        ConfigurePlayersClientRpc(players.Values.ToArray());
    }

    private void AddPlayer(NetworkObjectReference networkObjectReference) {
        players.TryAdd(networkObjectReference.NetworkObjectId,networkObjectReference);
        Debug.Log(players.Count);
    }

    [ClientRpc]
    private void ConfigurePlayersClientRpc(NetworkObjectReference[] networkObjectsReference) {
        if (IsServer) return;
        ConfigurePlayers(networkObjectsReference);
        Debug.Log(players.Count);
    }

    private void ConfigurePlayers(NetworkObjectReference[] networkObjectsReference) {
        foreach (var networkObjectReference in networkObjectsReference) {
            if(players.ContainsKey(networkObjectReference.NetworkObjectId))
                continue;
            
            players.Add(networkObjectReference.NetworkObjectId,networkObjectReference);
        }

        players.OrderBy((key) => key.Key);
    }
}

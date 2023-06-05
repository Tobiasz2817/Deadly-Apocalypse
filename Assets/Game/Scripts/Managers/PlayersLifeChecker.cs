using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayersLifeChecker : NetworkBehaviour
{
    public UnityEvent<ulong> OnLastSurvivor;
    private Dictionary<ulong,bool> isAlivePlayers = new Dictionary<ulong, bool>();
    
    [SerializeField]
    private PlayersReferenceData playersReferenceData;

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        playersReferenceData.OnPlayerSpawnAndConnected.AddListener(OnLoadPlayer);
    }

    public override void OnNetworkDespawn() {
        if (!IsServer) return;
        playersReferenceData.OnPlayerSpawnAndConnected.RemoveListener(OnLoadPlayer);
    }


    private void OnLoadPlayer() {
        var clients = playersReferenceData.GetValues();
        foreach (var client in clients) {
            var clientNetworkObject = (NetworkObject)client;
            if (isAlivePlayers.ContainsKey(clientNetworkObject.OwnerClientId)) continue;
        
            if (clientNetworkObject.TryGetComponent(out CharacterDeathListener deathListener)) 
                deathListener.OnPlayerDeath.AddListener(PlayerDeath);
        
            isAlivePlayers.Add(clientNetworkObject.OwnerClientId,false);
        }
    }

    public void PlayerDeath(ulong deathIndex) {
        if (isAlivePlayers.ContainsKey(deathIndex)) {
            isAlivePlayers[deathIndex] = false;
        }

        if (PlayersDead()) {
            var player = GetAlivePlayer();
            if(player != 99)
                OnLastSurvivor?.Invoke(player);
        }
    }

    public void SetPlayersAlive() {
        foreach (var player in isAlivePlayers) {
            isAlivePlayers[player.Key] = true;
        }
    }

    private bool PlayersDead() {
        int countAlive = 0;
        foreach (var player in isAlivePlayers) 
            if (player.Value) 
                countAlive++;

        if (countAlive > 1)
            return false;
        
        return true;
    }

    private ulong GetAlivePlayer() {
        foreach (var player in isAlivePlayers) 
            if (player.Value) 
                return player.Key;

        return 99;
    }
}

public interface ClientSpawnReceiver
{
    void SpawnedClient(NetworkObject client);
    int priority { set; get; }
}

public interface ClientDespawnReceiver
{
    void SpawnedClient(NetworkObject disconnectedClient);
}


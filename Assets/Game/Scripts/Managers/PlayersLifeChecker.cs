using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayersLifeChecker : NetworkBehaviour, ClientSpawnReceiver
{
    public UnityEvent<ulong> OnLastSurvivor;
    private Dictionary<ulong,bool> isAlivePlayers = new Dictionary<ulong, bool>();
    
    
    private void OnLoadPlayer(NetworkObject client) {
        if (isAlivePlayers.ContainsKey(client.OwnerClientId)) return;
        
        if (client.TryGetComponent(out CharacterDeathListener deathListener)) 
            deathListener.OnPlayerDeath.AddListener(PlayerDeath);
        
        isAlivePlayers.Add(client.OwnerClientId,false);
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

    [field:SerializeField] public int priority { get; set; }
    public void SpawnedClient(NetworkObject client) {
        OnLoadPlayer(client);
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


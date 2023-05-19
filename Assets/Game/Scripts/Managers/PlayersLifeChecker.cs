using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayersLifeChecker : NetworkBehaviour
{
    public UnityEvent<ulong> OnLastSurvivor;
    private Dictionary<ulong,bool> isAlivePlayers = new Dictionary<ulong, bool>();

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadPlayer;
    }
    
    public override void OnNetworkDespawn() {
        if (!NetworkManager.Singleton) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadPlayer;
    }

    private void OnLoadPlayer(ulong clientId, string sceneName, LoadSceneMode loadSceneMode) {
        if (isAlivePlayers.ContainsKey(clientId)) return;
        
        isAlivePlayers.Add(clientId,false);
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

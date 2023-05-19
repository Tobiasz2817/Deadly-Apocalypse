using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PvpRoundManager : NetworkBehaviour
{
    public UnityEvent OnRoundStart;

    private List<ulong> players = new List<ulong>();

    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadPlayer;
    }
    
    public override void OnNetworkDespawn() {
        if (!NetworkManager.Singleton) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadPlayer;
    }

    private void OnLoadPlayer(ulong clientid, string scenename, LoadSceneMode loadscenemode) {
        players.Add(clientid);
        
        if(players.Count == 2) RestartRound();
    }

    public void RestartRound() {
        OnRoundStart?.Invoke();
    }
}

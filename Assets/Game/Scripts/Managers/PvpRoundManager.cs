using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PvpRoundManager : NetworkBehaviour, ClientSpawnReceiver
{
    public UnityEvent OnRoundRestart;
    public UnityEvent OnRoundRestartAll;
    public UnityEvent OnRoundStart;
    private List<NetworkObject> players = new List<NetworkObject>();


    [SerializeField] private PvpRoundInterface pvpRoundInterface;
    
    public override void OnNetworkSpawn() {
        pvpRoundInterface.GetCountEndInvokers().AddListener(StartRound);
    }
    
    public override void OnNetworkDespawn() {
        pvpRoundInterface.GetCountEndInvokers().RemoveListener(StartRound);
    }

    private void OnLoadPlayer(NetworkObject client) {
        players.Add(client);

        if (players.Count == 2) {
            //RestartRound();
        }
    }
    
    private void RestartRound() { 
        Debug.Log("RESTART");
        SendClientRpc();
        OnRoundRestart?.Invoke();
    }


    [ClientRpc]
    private void SendClientRpc() {
        if (IsServer) return;
        OnRoundRestartAll?.Invoke();
    }

    private void StartRound() {
        OnRoundStart?.Invoke();
    }
    
    [field:SerializeField] public int priority { get; set; }
    public void SpawnedClient(NetworkObject client) {
        OnLoadPlayer(client);
    }
}


using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PvpRoundManager : NetworkBehaviour
{
    public UnityEvent OnRoundRestart;
    public UnityEvent OnRoundRestartAll;
    public UnityEvent OnRoundStart;

    [SerializeField] private PvpRoundInterface pvpRoundInterface;
    [SerializeField] private PlayersReferenceData playersReferenceData;
    
    public override void OnNetworkSpawn() {
        pvpRoundInterface.GetCountEndInvokers().AddListener(StartRound);
    }
    
    public override void OnNetworkDespawn() {
        pvpRoundInterface.GetCountEndInvokers().RemoveListener(StartRound);
    } 

    public void ClientsSpawn() {
        if(playersReferenceData.GetKeys().Count() == 2)
            RestartRound();
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
        if (!IsServer) return;
        OnRoundStart?.Invoke();
    }
}


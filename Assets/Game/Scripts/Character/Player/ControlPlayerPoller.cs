using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPlayerPoller : NetworkBehaviour
{
    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadPlayer;
        NetworkManager.Singleton.OnClientDisconnectCallback += Disconnect;
    }
    
    public override void OnNetworkDespawn() {
        if (!NetworkManager.Singleton) return;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadPlayer;
        NetworkManager.Singleton.OnClientDisconnectCallback -= Disconnect;
    }

    private void Disconnect(ulong obj) {
        if (!NetworkPoller.Instance) return;
        var key = NetworkPoller.Instance.GetOwnerByPlayerKey(obj, new[] { Owner.Player_1, Owner.Player_2, Owner.Player_3, Owner.Player_4});
        NetworkPoller.Instance.RemoveKeyClientRpc(key,10f);
    }

    private void OnLoadPlayer(ulong clientid, string scenename, LoadSceneMode loadscenemode) {
        StartCoroutine(CreatePlayerPoll(clientid));
    }
    
    private IEnumerator CreatePlayerPoll(ulong obj) {
        yield return new WaitUntil (() => NetworkPoller.Instance);
        foreach (var player in NetworkManager.Singleton.ConnectedClients) {
            NetworkPoller.Instance.InitPollerFromExistPollerDataClientRpc(player.Value.PlayerObject, player.Key,
                Owner.Player, Owner.Player_1, Owner.Player_2, Owner.Player_3, Owner.Player_4);
        }
    }
}

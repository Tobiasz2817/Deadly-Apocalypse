using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInitPoller : NetworkBehaviour
{
    private void Start() {
        NetworkPoller.Instance.InitPollerFromExistPollerDataServerRpc(NetworkObject,OwnerClientId,Owner.Player,Owner.Player_1,Owner.Player_2,Owner.Player_3,Owner.Player_4);

    }

    public override void OnNetworkSpawn() {
        StartCoroutine(CreatePlayerPoll());
    }

    public override void OnNetworkDespawn() {
        
    }

    private IEnumerator CreatePlayerPoll() {
        yield return new WaitUntil (() => NetworkPoller.Instance);
        //NetworkPoller.Instance.InitPollerFromExistPollerDataServerRpc(NetworkObject,OwnerClientId,Owner.Player,Owner.Player_1,Owner.Player_2,Owner.Player_3,Owner.Player_4);
    }
}

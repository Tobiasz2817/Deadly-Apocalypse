using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DisableWhenNotOwner : NetworkBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();

    public override void OnNetworkSpawn() {
        DisableScripts();
    }

    private void DisableScripts() {
        foreach (var script in scriptsToDisable)
            script.enabled = IsOwner;
    }
}

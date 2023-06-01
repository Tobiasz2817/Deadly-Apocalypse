using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterComponentsState : NetworkBehaviour
{
    [SerializeField] private List<MonoBehaviour> components = new List<MonoBehaviour>();

    public override void OnNetworkSpawn() {
        Debug.Log("Network spawn");
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetStateComponentsServerRpc(bool state) {
        SetStateComponentsClientRpc(state);
    }

    [ClientRpc]
    public void SetStateComponentsClientRpc(bool state) {
        SetStateComponents(state);
    }
    
    public void SetStateComponentsTmp(bool state) {
        if (!IsOwner) {
            return;
        }

        SetStateComponentsServerRpc(state);
    }

    public void SetStateComponents(bool state) {
        foreach (var component in components) {
            component.enabled = state;
        }
        
        Debug.Log("Is Owner: " + IsOwner);
    }
}

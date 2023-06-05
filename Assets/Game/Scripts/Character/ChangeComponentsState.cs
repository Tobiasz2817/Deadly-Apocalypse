using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class ChangeComponentsState : NetworkBehaviour
{
    [SerializeField]
    private PlayersReferenceData playersReferenceData;
    
    public void ChangeStates(bool state) {
        ChangeStatesClientRpc(playersReferenceData.GetValues().ToArray(), state);
    }
    
    [ClientRpc]
    public void ChangeStatesClientRpc(NetworkObjectReference[] players ,bool state) {
        var localPlayer = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        if (localPlayer.TryGetComponent(out CharacterComponentsState characterComponentsState)) {
            characterComponentsState.SetStateComponents(state);
        }
    }
}

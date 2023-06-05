using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayersStateController : NetworkBehaviour
{
    [SerializeField] private PlayersReferenceData playersReferenceData;

    public void ChangeClientsState(bool state) {
        ChangeClientsStateClientRpc(playersReferenceData.GetValues().ToArray(),state);
    }

    [ClientRpc]
    private void ChangeClientsStateClientRpc(NetworkObjectReference[] players ,bool state) {
        ChangeClientsStates(players, state);
    }

    private void ChangeClientsStates(NetworkObjectReference[] players ,bool state) {
        foreach (var key in players) {
            ChangeState(key,state);
        }
    }

    private void ChangeState(NetworkObject player, bool state) {
        player.gameObject.SetActive(state);
    }
}


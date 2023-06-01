using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChangeComponentsState : NetworkBehaviour, ClientSpawnReceiver
{
    private Dictionary<ulong, CharacterComponentsState> players = new Dictionary<ulong, CharacterComponentsState>();

    private NetworkObjectReference componentsState;


    private IEnumerator WiatForPlayer() {
        NetworkObject obj;
        do {
            obj = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            yield return new WaitForSeconds(0.1f);
        } while (obj == null);
    }

    public void ChangesCharacterStates(bool state) {

        var player = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        if (player.TryGetComponent(out CharacterComponentsState characterComponentsState)) {
            characterComponentsState.SetStateComponents(state);
        }

        /*foreach (var player in players) {
            player.Value.SetStateComponents(state);
        }*/
    }

    private void AddPlayer(NetworkObject client) {
        players.TryAdd(client.OwnerClientId, client.GetComponent<CharacterComponentsState>());
    }

    [field:SerializeField] public int priority { get; set; }
    public void SpawnedClient(NetworkObject client) {
        Debug.Log("AddClient");
        AddPlayer(client);
    }
}

using Unity.Netcode;
using UnityEngine.Events;

public class CharacterDeathListener : NetworkBehaviour
{
    public UnityEvent<ulong> OnPlayerDeath;

    public void CheckHealth(float health) {
        if (health <= 0) {
            OnPlayerDeath?.Invoke(NetworkObjectId);
        }
    }
}
 
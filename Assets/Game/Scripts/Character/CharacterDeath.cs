using Unity.Netcode;

public class CharacterDeath : NetworkBehaviour
{
    private NetworkVariable<bool> isDead = new NetworkVariable<bool>(false,NetworkVariableReadPermission.Owner,NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn() {
        isDead.OnValueChanged += PlayerDead;
    }
    
    public override void OnNetworkDespawn() {
        isDead.OnValueChanged -= PlayerDead;
    }
    
    private void PlayerDead(bool previousvalue, bool newvalue) {
        gameObject.SetActive(newvalue);
    }

    public void SetPlayerDead() {
        isDead.Value = false;
    }
    
    public void SetPlayerAlive() {
        isDead.Value = true;
    }
}

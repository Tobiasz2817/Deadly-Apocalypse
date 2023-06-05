using Unity.Netcode;

public class DisableObjectAtNetworkSpawn : NetworkBehaviour
{
    public override void OnNetworkSpawn() {
        //gameObject.SetActive(false);
    }
}

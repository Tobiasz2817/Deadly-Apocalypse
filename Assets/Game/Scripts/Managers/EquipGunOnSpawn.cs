using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class EquipGunOnSpawn : NetworkBehaviour
{
    [SerializeField] private WeaponHandler weaponHandler;
    
    public override void OnNetworkSpawn() {
        if (!IsServer) return;
        StartCoroutine(EquipGunCoroutine());
    }
    
    private IEnumerator EquipGunCoroutine() {
        yield return new WaitUntil(() => NetworkPoller.Instance);
        var gunIndex = NetworkPoller.Instance.GetIndexObject(Owner.Everyone, ObjectPollTypes.Guns, typeof(BasicGun));
        EquipGunClientRpc(gunIndex,Owner.Everyone);
    }

    [ClientRpc]
    private void EquipGunClientRpc(int gunIndex, Owner owner) {
        var gun = NetworkPoller.Instance.GetObject(owner, ObjectPollTypes.Guns, gunIndex);
        weaponHandler.EquipGun((Gun)gun);
    }
}

using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PickUpGun : NetworkBehaviour
{
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private float range;
    [SerializeField] private Transform point;
    
    [SerializeField] private PlayerInput playerInput;

    public UnityEvent<Gun> OnEnterPickUpGun;
    public UnityEvent OnEnterSwampGun;
    
    private void OnEnable() {
        playerInput.OnPickUpButtonPressed += CheckPickUp;
    }

    private void OnDisable() {
        playerInput.OnPickUpButtonPressed -= CheckPickUp;
    }
    
    private void CheckPickUp() {
        var gun = GetOverlap<Gun>();
        if (!gun) {
            HandlingGunActionServerRpc(true,Owner.Everyone,0);
            HandlingGunAction(true,Owner.Everyone,0);
        }
        else {
            HandlingGunActionServerRpc(false,Owner.Everyone,gun.objectId);
            HandlingGunAction(false,Owner.Everyone,gun.objectId);
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void HandlingGunActionServerRpc(bool swampGun, Owner ownerType, int gunId) {
        HandlingGunActionClientRpc(swampGun,ownerType,gunId);
    }
    
    [ClientRpc]
    private void HandlingGunActionClientRpc(bool swampGun, Owner ownerType, int gunId) {
        if (IsOwner) return;
        HandlingGunAction(swampGun,ownerType,gunId);
    }
    
    private void HandlingGunAction(bool swampGun, Owner ownerType, int gunId) {
        var gun = (Gun)NetworkPoller.Instance.GetActiveObject(ownerType, ObjectPollTypes.Guns, gunId);
        if (!swampGun) OnEnterPickUpGun?.Invoke(gun);
        else OnEnterSwampGun?.Invoke();
    }

    public T GetOverlap<T>() where T : Gun {
        var colliders = Physics2D.OverlapCircle(point.position, range, pickUpLayerMask);
        return colliders == null ? null : colliders.GetComponent<T>();
    }
}
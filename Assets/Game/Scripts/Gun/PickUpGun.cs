using System;
using UnityEngine;
using UnityEngine.Events;

public class PickUpGun : MonoBehaviour
{
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private float range;
    [SerializeField] private Transform point;
    
    [SerializeField] private PlayerInput playerInput;

    public UnityEvent<Gun> OnEnterPickUpGun;
    public UnityEvent OnEnterSwampGun;
    
    private void OnEnable() {
        playerInput.OnPickUpButtonPressed += HandlingGunAction;
    }

    private void OnDisable() {
        playerInput.OnPickUpButtonPressed -= HandlingGunAction;
    }
    
    private void HandlingGunAction() {
        var gun = GetOverlap<Gun>();
        if (gun) OnEnterPickUpGun?.Invoke(gun);
        else OnEnterSwampGun?.Invoke();
    }

    public T GetOverlap<T>() where T : Gun {
        var colliders = Physics2D.OverlapCircle(point.position, range, pickUpLayerMask);
        return colliders == null ? null : colliders.GetComponent<T>();
    }
}
/*public UnityEvent<Gun> OnEnterPickUpGun;
public UnityEvent OnStayPickUpGun;
public UnityEvent<Gun> OnExitPickUpGun;

public void OnTriggerEnter2D(Collider2D other) {
    if (other.TryGetComponent(out Gun gun)) 
        OnEnterPickUpGun?.Invoke(gun);
}

private void OnTriggerStay(Collider other) {
    OnStayPickUpGun?.Invoke();
}


public void OnTriggerExit2D(Collider2D other) {
    if (other.TryGetComponent(out Gun gun)) 
        OnExitPickUpGun?.Invoke(gun);
}*/
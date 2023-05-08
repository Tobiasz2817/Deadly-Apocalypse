using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform pickUpGunParent;
    [SerializeField] private Transform gunOffsetTransform;
    [SerializeField] private int gunCount = 2;

    private List<Gun> guns = new List<Gun>();

    private int currentIndex = 0;
    
    public UnityEvent<Gun> OnGunChange;

    private void Start() {
        for (int i = 0; i < gunCount; i++) 
            guns.Add(null);
    }


    public void SwampGun() {
        var propIndex = currentIndex + 1;
        var lastIndex = currentIndex;
        if (IsInArrayRange(propIndex))
            if (guns[propIndex] != null)
                SetIndex(propIndex);
            else
                return;
        else 
            SetIndex(0);

        EquipGun(guns[currentIndex],currentIndex,lastIndex);
        OnGunChange?.Invoke(guns[currentIndex]);
    }

    private bool IsInArrayRange(int index) {
        if (index >= 0 && index < guns.Count)
            return true;

        return false;
    }

    public void EquipGun(Gun gun) {
        var freeIndex = currentIndex;
        for (int i = 0; i < guns.Count; i++) {
            if (guns[i] == null) {
                freeIndex = i;
                break;
            }
        }

        EquipGun(gun, freeIndex, currentIndex);
    }

    public void EquipGun(Gun gun, int currentIndex, int lastIndex) {
        TryUnEquipGun(guns[lastIndex]);
        SetReferenceGun(gun,currentIndex);
        ChangeGunColliderState(guns[currentIndex], false);
        SetGunParent(guns[currentIndex]);
        SetGunPosition(guns[currentIndex]);
        TurnOnGunState(guns[currentIndex], true);
        SetIndex(currentIndex);
    }

    private void SetIndex(int index) {
        this.currentIndex = index;
    }
    
    public void TryUnEquipGun(Gun gun) {
        if (gun == null) return;
        SinglePoller.Instance.PollObject(gun.OwnerType,gun.Type,gun.objectId);
        ChangeGunColliderState(gun, true);
    }

    private void TurnOnGunState(Gun gun,bool state) {
        gun.gameObject.SetActive(state);
    }
    
    private void SetReferenceGun(Gun gun, int index) {
        guns[index] = gun;
    }

    private void SetGunPosition(Gun gun) {
        gun.transform.localPosition = gunOffsetTransform.localPosition;
        gun.transform.localRotation = gunOffsetTransform.localRotation;
        gun.transform.localScale = gunOffsetTransform.localScale;
    }

    private void SetGunParent(Gun gun) {
        gun.transform.SetParent(pickUpGunParent);
    }
    
    private void ChangeGunColliderState(Gun gun, bool isActive) {
        gun.ChangeStateCollider(isActive);   
    }
}

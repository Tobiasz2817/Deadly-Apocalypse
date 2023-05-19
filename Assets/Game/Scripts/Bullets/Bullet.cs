using System;
using System.Collections;
using UnityEngine;

public abstract class Bullet : PolledObject
{
    private bool canMove;
    protected ShootDependencies shootDependencies;

    [SerializeField] private Transform bulletBody;
    [SerializeField] private float pollTime = 5f;

    private void OnEnable() {
        StartCoroutine(PollAfterTime(pollTime));
    }

    public void SetShootDependencies(ShootDependencies shootDependencies) {
        this.shootDependencies = shootDependencies;
    }

    public ShootDependencies GetShootDependencies => shootDependencies;
    
    public void MoveTowards(Vector2 direction_) {
        this.shootDependencies.direction = direction_;

        canMove = true;
    }

    public void RotateTo() {
        Vector3 lookDirection = shootDependencies.direction - (Vector2)shootDependencies.shootPoint;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 180;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        bulletBody.rotation = rotation;
    }
    
    protected abstract void MoveTo(Vector2 direction);
    private void Update() { if (!canMove) return; MoveTo(shootDependencies.direction); }

    private IEnumerator PollAfterTime(float time) {
        yield return new WaitForSeconds(time);
        if(NetworkPoller.Instance == null) yield break;
        NetworkPoller.Instance.PollObject(OwnerType,Type,objectId);
        canMove = false;
    }
}

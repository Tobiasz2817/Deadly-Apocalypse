using System;
using System.Collections;
using UnityEngine;

public abstract class Bullet : PolledObject
{
    private bool canMove;
    private Vector2 direction;

    [SerializeField] private Transform bulletBody;
    [SerializeField] private float pollTime = 5f;

    private void OnEnable() {
        StartCoroutine(PollAfterTime(pollTime));
    }

    public void MoveTowards(Vector2 direction_) {
        this.direction = direction_;

        canMove = true;
    }

    public void RotateTo(Transform shootPoint) {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        Vector3 lookDirection = worldPosition - shootPoint.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 180;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        bulletBody.rotation = rotation;
    }
    
    public void RotateTo(Vector2 dir,Transform shootPoint) {
        Quaternion rotation = Quaternion.LookRotation(-shootPoint.right, Vector3.forward);
        bulletBody.rotation = rotation;
    }

    public void RotateTo(Vector3 direction) {
        Vector3 rotationDirection = new Vector3(direction.x, direction.y, 0);
        float rotationAngle = Vector3.SignedAngle(Vector3.right, rotationDirection, Vector3.forward) + 180;

        // wykonaj rotację broni na osi Z
        bulletBody.localRotation = Quaternion.Euler(0.0f, 0.0f, rotationAngle);
    }
    
    public abstract void MoveTo(Vector2 direction);
    private void FixedUpdate() { if (!canMove) return; MoveTo(direction); }

    private IEnumerator PollAfterTime(float time) {
        yield return new WaitForSeconds(time);
        if(SinglePoller.Instance == null) yield break;
        SinglePoller.Instance.PollObject(OwnerType,Type,objectId);
    }
}

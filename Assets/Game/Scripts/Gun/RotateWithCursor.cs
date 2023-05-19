using Unity.Netcode;
using UnityEngine;

public class RotateWithCursor : NetworkBehaviour
{
    [SerializeField] private float minDistance = 0.25f;
    [SerializeField] private float offset = 180f;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private UnitInput unitInput;

    private Vector2 mouseInput;
    private void Start() {
        if (targetPoint == null) targetPoint = transform;
    }

    private void OnEnable() {
        unitInput.OnChangeInputLookAt += ReadInputs;
    }
    private void OnDisable() {
        unitInput.OnChangeInputLookAt -= ReadInputs;
    }
    
    private void ReadInputs(Vector2 input) {
        var pos = Camera.main.ScreenToWorldPoint(input);
        pos.z = 0;
        if (Vector3.Distance(pos, transform.position) < minDistance) return;
        mouseInput = input;
    }

    private void Update() {
        if (mouseInput == Vector2.zero) return;
        RotateTo(mouseInput);
    }
    
    private void RotateTo(Vector2 direction_) {
        var direction =  (Camera.main.ScreenToWorldPoint(direction_) - targetPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - offset;
        transform.localRotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    public void SetOffset(float offset_) {
        this.offset = offset_;
    }

    public void SetTargetPoint(Gun gun) {
        this.targetPoint = gun.GetLookAtPoint();
        if(IsOwner)
            this.enabled = true;
    }
}
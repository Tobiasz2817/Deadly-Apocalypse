using Unity.Netcode;
using UnityEngine;

public class ScaleWithMousePosition : NetworkBehaviour
{
    [SerializeField] private Transform targetScale;
    [SerializeField] private UnitInput unitInput;
    
    private Vector2 mouseInput;

    [SerializeField] private bool modifyScaleX;
    [SerializeField] private float leftSideX;
    [SerializeField] private float rightSideX;

    [SerializeField] private bool modifyScaleY;
    [SerializeField] private float leftSideY;
    [SerializeField] private float rightSideY;

    private bool lastX = false;
    
    private void OnEnable() {
        unitInput.OnChangeInputLookAt += ReadInputs;
    }
    private void OnDisable() {
        unitInput.OnChangeInputLookAt -= ReadInputs;
    }
    
    private void ReadInputs(Vector2 input) {
        if (input == Vector2.zero) return;
        mouseInput = Camera.main.ScreenToWorldPoint(input);
    }

    private void Update() {
        var isMouseRightOfPlayer = mouseInput.x >= targetScale.position.x;
        if (lastX == isMouseRightOfPlayer) return;

        ChangeScaleServerRpc(isMouseRightOfPlayer);
        ChangeScale(isMouseRightOfPlayer);
        lastX = isMouseRightOfPlayer;
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeScaleServerRpc(bool isMouseRightOfPlayer) {
        ChangeScaleClientRpc(isMouseRightOfPlayer);
    }
    [ClientRpc]
    private void ChangeScaleClientRpc(bool isMouseRightOfPlayer) {
        if (IsOwner) return;
        ChangeScale(isMouseRightOfPlayer);
    }
    private void ChangeScale(bool isMouseRightOfPlayer) {
        var leftSideX_ = modifyScaleX ? leftSideX : targetScale.localScale.x;
        var rightSideX_ = modifyScaleX ? rightSideX : targetScale.localScale.x;
        
        var leftSideY_ = modifyScaleY ? leftSideY : targetScale.localScale.y;
        var rightSideY_ = modifyScaleY ? rightSideY : targetScale.localScale.y;
        
        if (isMouseRightOfPlayer) targetScale.localScale = new Vector3(rightSideX_, rightSideY_);
        else targetScale.localScale = new Vector3(leftSideX_, leftSideY_);
    }
}

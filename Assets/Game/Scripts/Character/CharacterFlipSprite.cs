using Unity.Netcode;
using UnityEngine;

public class CharacterFlipSprite : NetworkBehaviour
{
    [SerializeField] private UnitInput unitInput;
    [SerializeField] private SpriteRenderer characterSprite;

    private bool lastFlipX;
    private void OnEnable() {
        unitInput.OnChangeInputMovement += UpdateCharacterSprite;
    }

    private void OnDisable() {
        unitInput.OnChangeInputMovement -= UpdateCharacterSprite;
    }

    private void UpdateCharacterSprite(Vector2 input) {
        if (input == Vector2.zero) return;
        if (input.y != 0 && input.x == 0) return;
        lastFlipX = Mathf.RoundToInt(input.x) > 0;
        if (lastFlipX == characterSprite.flipX) return;
        
        SyncFlipServerRpc(lastFlipX);
        SyncFlip(lastFlipX);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SyncFlipServerRpc(bool flipState) {
        SyncFlipClientRpc(flipState);
    }
    
    [ClientRpc]
    private void SyncFlipClientRpc(bool flipState) {
        if (IsOwner) return;
        SyncFlip(flipState);
    }
    
    private void SyncFlip(bool flipState) {
        characterSprite.flipX = flipState;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlipSprite : MonoBehaviour
{
    [SerializeField] private UnitInput unitInput;
    [SerializeField] private SpriteRenderer characterSprite;

    private void OnEnable() {
        unitInput.OnChangeInputMovement += UpdateCharacterSprite;
    }

    private void OnDisable() {
        unitInput.OnChangeInputMovement -= UpdateCharacterSprite;
    }

    private void UpdateCharacterSprite(Vector2 input) {
        if (input == Vector2.zero) return;
        characterSprite.flipX = Mathf.RoundToInt(input.x) > 0;
    }
}

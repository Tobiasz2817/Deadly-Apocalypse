using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
     [SerializeField] private UnitInput unitInput;
     [SerializeField] private Animator characterAnimator;
     
     private void Update() {
          characterAnimator.SetInteger("MoveXInt",Mathf.RoundToInt(unitInput.Input.x));
          characterAnimator.SetInteger("MoveYInt",Mathf.RoundToInt(unitInput.Input.y));
          characterAnimator.SetBool("Sprint",unitInput.Sprint);
     }
}

using System.Collections;
using TMPro;
using UnityEngine;

namespace SceneAnimation
{
    public class AnimateDotText : MonoBehaviour
    {
        [SerializeField] private int lengthLoadingDot = 3;
        [SerializeField] private TMP_Text loadingText;

        [SerializeField] private bool animOnStart = false;
        
        private void OnEnable() {
            if(animOnStart) StartCoroutine(LoadingText());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator LoadingText() {
            while (true) {
                string startedText = loadingText.text;
                for (int i = lengthLoadingDot; i >= 0; i--) {
                    yield return new WaitForSeconds(0.75f);
                    loadingText.text += " " + ".";
                }

                loadingText.text = startedText;
            }
        }
    }
}
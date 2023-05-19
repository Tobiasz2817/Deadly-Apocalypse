using UnityEngine;

public class PlugCameraToCanvas : MonoBehaviour
{
    [SerializeField] private Canvas plugInCanvas;
    private void Start() {
        plugInCanvas.worldCamera = Camera.main;
    }
}

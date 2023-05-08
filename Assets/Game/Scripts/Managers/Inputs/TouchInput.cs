using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action<PointerEventData> OnTouchDown;
    public event Action<PointerEventData> OnTouchUp;
    public event Action<PointerEventData> OnTouchDrag;

    public void OnPointerDown(PointerEventData eventData) {
        OnTouchDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnTouchUp?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        OnTouchDrag?.Invoke(eventData);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[AddComponentMenu("Input/On Move Joystick")]
public class OnScreenJoystick : OnScreenControl
{
    [SerializeField] private TouchInput touchInput;
    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 50;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string modifyPath;

    private Vector3 m_StartPos;
    private Vector2 m_PointerDownPos;

    private Image handler;
    public float movementRange
    {
        get => m_MovementRange;
        set => m_MovementRange = value;
    }

    private void Awake() {
        handler = GetComponent<Image>();
    }

    private new void OnEnable() {
        base.OnEnable();
        touchInput.OnTouchDown += OnPointerDown;
        touchInput.OnTouchUp += OnPointerUp;
        touchInput.OnTouchDrag += OnDrag;
    }

    private new void OnDisable() {
        base.OnDisable();
        touchInput.OnTouchDown -= OnPointerDown;
        touchInput.OnTouchUp -= OnPointerUp;
        touchInput.OnTouchDrag -= OnDrag;
    }

    protected override string controlPathInternal
    {
        get => modifyPath;
        set => modifyPath = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        ControlStateObject(true);
        ModifyStartPos(eventData.position);
        SetNewPosition(eventData.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
        var delta = position - m_PointerDownPos;

        delta = Vector2.ClampMagnitude(delta, movementRange);
        ((RectTransform)transform).position = m_StartPos + (Vector3)delta;

        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        SendValueToControl(newPos);
    }

    public void OnPointerUp(PointerEventData eventData) {
        SetNewPosition(m_StartPos);
        ControlStateObject(false);
        SendValueToControl(Vector2.zero);
    }
    
    private void ModifyStartPos(Vector3 newPos) {
        this.m_StartPos = newPos;
    }

    private void SetNewPosition(Vector3 newPos) {
        ((RectTransform)transform).position = newPos;
    }

    private void ControlStateObject(bool isActive) {
        handler.enabled = isActive;
    }
}
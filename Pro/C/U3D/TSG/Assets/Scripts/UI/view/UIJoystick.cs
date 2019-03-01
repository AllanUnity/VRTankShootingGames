using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle;

    public JoystickMode joystickMode = JoystickMode.AllAxis;

    private Vector2 inputVector = Vector2.zero;
    /// <summary>拖动的最大长度</summary>
    public float dragMagnitude;

    /// <summary>拖动中心</summary>
    private Vector2 joystickCenter = Vector2.zero;

    public delegate void JoystickDelegate(Vector2 vector);
    /// <summary>拖动事件</summary>
    public event JoystickDelegate JoystickEvent;
    public Vector3 displacement;
    void Start()
    {
        joystickCenter = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 direction = eventData.position - joystickCenter;
        float magnitude = direction.magnitude;//拖动长度
        inputVector = (magnitude > dragMagnitude) ? direction.normalized : direction / (dragMagnitude);
        ClampJoystick();
        handle.anchoredPosition = inputVector * dragMagnitude;

        JoystickEvent(inputVector);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 vector3 =new Vector3( eventData.position.x - displacement.x,eventData.position.y-displacement.y);
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        JoystickEvent(inputVector);
    }
    protected void ClampJoystick()
    {
        if (joystickMode == JoystickMode.Horizontal)
            inputVector = new Vector2(inputVector.x, 0f);
        if (joystickMode == JoystickMode.Vertical)
            inputVector = new Vector2(0f, inputVector.y);
    }
}



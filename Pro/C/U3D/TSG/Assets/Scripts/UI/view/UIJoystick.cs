using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Components")]
    public RectTransform background;
    public RectTransform handle;

    [Header("Variable Joystick Options")]
    public bool isFixed = true;

    [Header("Joystick Model")]
    public JoystickMode joystickMode=JoystickMode.AllAxis;

    private Vector2 inputVector = Vector2.zero;
    /// <summary>拖动的最大长度</summary>
    private float dragMagnitude;

    /// <summary>拖动中心</summary>
    private Vector2 joystickCenter = Vector2.zero;

    public delegate void JoystickDelegate(Vector2 vector);
    /// <summary>拖动事件</summary>
    public event JoystickDelegate JoystickEvent;

    void Start()
    {
        dragMagnitude = background.sizeDelta.x / 2;
        if (isFixed)
        {
            handle.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);
        }
        else
        {
            joystickCenter = Vector2.zero;
            background.gameObject.SetActive(true);
            handle.anchoredPosition = Vector2.zero;
            background.anchoredPosition = Vector2.zero;
        }
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
        if (isFixed)
        {
            background.gameObject.SetActive(true);
            background.position = eventData.position;
            handle.anchoredPosition = Vector2.zero;
        }
        joystickCenter = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFixed)
        {
            background.gameObject.SetActive(false);
        }
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



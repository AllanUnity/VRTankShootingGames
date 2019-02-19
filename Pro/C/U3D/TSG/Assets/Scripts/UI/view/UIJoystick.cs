using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Options")]
    /// <summary>拖动限制</summary>
    [Range(0f, 2f)]
    public float handleLimit = 1f;
    /// <summary>拖动方向限制</summary>
    public JoystickMode joystickMode = JoystickMode.AllAxis;

    /// <summary>操作的位置</summary>
    protected Vector2 inputVector = Vector2.zero;

    /// <summary>操作的背景</summary>
    [Header("Components")]
    public RectTransform background;
    /// <summary>操作的手</summary>
    public RectTransform handle;

    public float Horizontal { get { return inputVector.x; } }
    public float Vertical { get { return inputVector.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    protected void ClampJoystick()
    {
        if (joystickMode == JoystickMode.Horizontal)
            inputVector = new Vector2(inputVector.x, 0f);
        if (joystickMode == JoystickMode.Vertical)
            inputVector = new Vector2(0f, inputVector.y);
    }


    /// <summary>是否隐藏</summary>
    [Header("Variable Joystick Options")]
    public bool isFixed = true;
    /// <summary>初始位置</summary>
    public Vector2 fixedScreenPosition;
    Vector2 joystickCenter = Vector2.zero;

    void Start()
    {
        if (isFixed)
            OnFixed();
        else
            OnFloat();
    }

    public void ChangeFixed(bool joystickFixed)
    {
        if (joystickFixed)
            OnFixed();
        else
            OnFloat();
        isFixed = joystickFixed;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!isFixed)
        {
            background.gameObject.SetActive(true);
            background.position = eventData.position;
            handle.anchoredPosition = Vector2.zero;
            joystickCenter = eventData.position;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!isFixed)
        {
            background.gameObject.SetActive(false);
        }
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    void OnFixed()
    {
        joystickCenter = fixedScreenPosition;
        background.gameObject.SetActive(true);
        handle.anchoredPosition = Vector2.zero;
        background.anchoredPosition = fixedScreenPosition;
    }

    void OnFloat()
    {
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
    }
}

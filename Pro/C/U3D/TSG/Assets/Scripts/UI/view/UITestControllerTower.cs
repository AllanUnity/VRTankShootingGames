using System;
using UnityEngine;
using UnityEngine.UI;

public class UITestControllerTower : MonoBehaviour
{
    /// <summary>炮塔</summary>
    public TKTowerModelBase tower;
    public TKAbramsFireController fireController;
    /// <summary>摇杆</summary>
    public UIJoystick joystick;
    /// <summary>炮塔角度输入</summary>
    public InputField towerInputField;
    /// <summary>炮管角度输入</summary>
    public InputField gunInputField;
    /// <summary>确定</summary>
    public Button countersignBtn;

    public Button fireBtn;

    void Start()
    {
        countersignBtn.onClick.AddListener(() =>
        {
            float towerF = float.Parse(towerInputField.text);
            float gunF = float.Parse(gunInputField.text);

            tower.RotateTower(new Vector2(towerF, gunF));
        });
        joystick.JoystickEvent += JoytickCallBack;
        fireBtn.onClick.AddListener(FireOnClick);
    }

    private void FireOnClick()
    {
        fireController.Fire();
    }

    private void JoytickCallBack(Vector2 vector)
    {
        tower.RotateTower(vector.x,vector.y);
    }
}

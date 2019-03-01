using UnityEngine;
using UnityEngine.UI;

public class UITankController : UIBase
{
    /// <summary>炮塔</summary>
    public TKTowerController tower;
    /// <summary>发射单元</summary>
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

    public override void Init()
    {
        base.Init();
        countersignBtn.onClick.AddListener(() =>
        {
            float towerF = float.Parse(towerInputField.text);
            float gunF = float.Parse(gunInputField.text);
            if (tower != null)
            {
                tower.RotateTower(new Vector2(towerF, gunF));
            }
        });
        joystick.JoystickEvent += JoytickCallBack;
        fireBtn.onClick.AddListener(FireOnClick);
        tower = CSGame.Instance.tkController.towerController;
        fireController = CSGame.Instance.tkController.fireController;
    }

    private void FixedUpdate()
    {

    }
    private void FireOnClick()
    {
        fireController.Fire(BulletType.Default0);
    }

    private void JoytickCallBack(Vector2 vector)
    {
        if (tower != null)
        {
            tower.RotateTower(vector.x, vector.y);
        }
    }
}

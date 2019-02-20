using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestControllerTower : MonoBehaviour
{
    public TKTowerModelBase tower;
    public UIJoystick joystick;
    public InputField towerInputField;
    public InputField gunInputField;
    public Button countersignBtn;
    // Start is called before the first frame update
    void Start()
    {
        countersignBtn.onClick.AddListener(() =>
        {
            float towerF = float.Parse(towerInputField.text);
            float gunF = float.Parse(gunInputField.text);

            tower.RotateTower(new Vector2(towerF, gunF));
        });
        joystick.JoystickEvent += JoytickCallBack;
    }

    private void JoytickCallBack(Vector2 vector)
    {
        float h = vector.x;
        float v = vector.y;
        RotateType hType = RotateType.Stop;
        RotateType vType = RotateType.Stop;
        if (h<0)
        {
            hType=(RotateType.Left);
        }
        if (h>0)
        {
            hType=(RotateType.Right);
        }
        if (h==0)
        {
            hType=(RotateType.Stop);
        }
        if (v<0)
        {
            vType = RotateType.Down;
        }
        if (v>0)
        {
            vType = RotateType.Up;
        }
        if (v==0)
        {
            vType = RotateType.Stop;
        }
        tower.RotateTower(hType, vType);
    }
}

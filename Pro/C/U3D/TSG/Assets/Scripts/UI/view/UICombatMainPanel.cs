using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>战斗主界面</summary>
public class UICombatMainPanel : UIBase
{
    #region UIBase
    public override void Init()
    {
        base.Init();
        CSTimeManager.Singleton.GetCurrentTimeEvent -= ShowCurrentTime;
        CSTimeManager.Singleton.GetCurrentTimeEvent += ShowCurrentTime;
        InitController();
    }
    public override void Hide()
    {
        base.Hide();
        CSTimeManager.Singleton.GetCurrentTimeEvent -= ShowCurrentTime;
    }
    public override void Close()
    {
        base.Close();
        CSTimeManager.Singleton.GetCurrentTimeEvent -= ShowCurrentTime;
    }
    #endregion
    #region 比分
    /// <summary>得分</summary>
    public Text scoreText;


    #endregion
    #region 坦克信息
    /// <summary>坦克前方摄像头</summary>
    public RawImage TankFrontRawImage;
    private void InitTankMessage()
    {
        TankFrontRawImage.texture = CSGame.Instance.tkController.sightTexture;
    }
    #endregion

    #region 瞄准信息
    /// <summary>当前瞄准点</summary>
    public Image currentSight;
    /// <summary>目标瞄准点</summary>
    public Image targetSight;

    #endregion

    #region Time
    /// <summary>战斗倒计时</summary>
    public Text combatTimeText;
    /// <summary>当前时间</summary>
    public Text currentTimeText;
    /// <summary>展示当前时间</summary>
    /// <param name="currentTime"></param>
    private void ShowCurrentTime(DateTime currentTime)
    {
        currentTimeText.text = currentTime.ToString("MM.dd HH:mm");
    }
    #endregion

    #region 坦克控制
    /// <summary>炮塔控制器</summary>
    public UIJoystick joystick;
    /// <summary>开火1按钮</summary>
    public Button fire1Btn;
    /// <summary>开火2按钮</summary>
    public Button fire2Btn;
    public void InitController()
    {
        joystick.JoystickEvent += (vector) =>
        {
            CSGame.Instance.tkController.towerController.RotateTower(vector.x, vector.y);
        };
        fire1Btn.onClick.AddListener(() =>
        {
            CSGame.Instance.tkController.fireController.Fire(BulletType.Default0);
        });
        fire2Btn.onClick.AddListener(() =>
        {
            CSGame.Instance.tkController.fireController.Fire(BulletType.Default1);
        });
    }

    #endregion
}

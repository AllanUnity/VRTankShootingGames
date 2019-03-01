using System;
using UnityEngine.UI;

/// <summary>战斗主界面</summary>
public class UICombatMainPanel : UIBase
{
    private CSTimeManager timeManager;
    #region UIBase
    public override void Init()
    {
        base.Init();
        timeManager = CSTimeManager.Singleton;
        timeManager.GetCurrentTimeEvent -= ShowCurrentTime;
        timeManager.GetCurrentTimeEvent += ShowCurrentTime;
        InitController();
        InitTankMessage();
    }
    public override void Hide()
    {
        base.Hide();
        timeManager.GetCurrentTimeEvent -= ShowCurrentTime;
    }
    public override void Close()
    {
        base.Close();
        timeManager.GetCurrentTimeEvent -= ShowCurrentTime;
    }
    #endregion
    #region 比分
    /// <summary>得分</summary>
    public Text scoreText;


    #endregion
    #region 坦克信息
    /// <summary>坦克血量</summary>
    public Image HPImage;
    /// <summary>设置血量</summary>
    /// <param name="currentHP"></param>
    /// <param name="maxHP"></param>
    private void TankHP(int currentHP, int maxHP)
    {
        HPImage.fillAmount = currentHP / maxHP;
    }
    /// <summary>坦克前方摄像头</summary>
    public RawImage TankFrontRawImage;
    private void InitTankMessage()
    {
        CSPlayerManager.Singleton.GetSight(TankFrontRawImage);
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
    public void InitController()
    {
        joystick.JoystickEvent += CSPlayerManager.Singleton.RotateTower;
        fire1Btn.onClick.AddListener(CSPlayerManager.Singleton.Fire);
    }

    #endregion
}

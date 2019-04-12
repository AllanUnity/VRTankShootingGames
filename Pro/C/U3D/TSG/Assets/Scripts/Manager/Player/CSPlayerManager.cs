using UnityEngine;
using UnityEngine.UI;

/// <summary>角色控制管理类</summary>
public class CSPlayerManager : CSMonoSingleton<CSPlayerManager>, IOnUpdate
{
    public override void Init()
    {
        base.Init();
        CSTimeManager.Singleton.AddUpdate(this);
        //InitPlayer();
    }

    private TKController tkController;
    private RenderTexture tkSightTexture = null;
    /// <summary>控制方式选择</summary>
    private TKControllerType controllerType = TKControllerType.Joystick;
    /// <summary>初始化角色</summary>
    private void InitPlayer()
    {
        GameObject Player_Abrams = Instantiate(CSGame.Instance.GetStaticObj("Player_Abrams"));
        Player_Abrams.name = "Protagonist";
        Player_Abrams.transform.position = Vector3.zero;
        tkController = Player_Abrams.GetComponent<TKController>();
        tkController.Init();
        ChangeReadyNextBullet(BulletType.Default0);
        UIManager.Singleton.OpenPanel<UICombatMainPanel>();
    }

    /// <summary>获取前方摄像头</summary>
    /// <param name="image"></param>
    public void GetSight(RawImage image)
    {
        image.texture = tkSightTexture;
    }
    /// <summary>旋转</summary>
    public void RotateTower(Vector2 vec)
    {
        if (tkController!=null)
        {
            switch (controllerType)
            {
                case TKControllerType.None:
                    break;
                case TKControllerType.Joystick:
                    tkController.towerController.RotateTower(vec.x, vec.y);
                    break;
                case TKControllerType.Keyboard:
                    break;
                case TKControllerType.Phone:
                    break;
            }
        }
    }

    /// <summary>开火</summary>
    public void Fire()
    {
        if (tkController != null)
        {
            switch (controllerType)
            {
                case TKControllerType.None:
                    break;
                case TKControllerType.Joystick:
                    tkController.fireController.Fire();
                    break;
                case TKControllerType.Keyboard:
                    break;
                case TKControllerType.Phone:
                    break;
            }
        }
    }
    /// <summary>更改下一发炮弹的种类</summary>
    /// <param name="bt"></param>
    public void ChangeReadyNextBullet(BulletType bt)
    {
        if (tkController != null)
        {
            tkController.fireController.ReadyNextBullet(bt);
        }
    }

    public void OnUpdate(float time)
    {
        if (tkController != null)
        {
            tkController.OnUpdate(time);
        }
    }

    public void OnFixedUpdate()
    {

    }
}

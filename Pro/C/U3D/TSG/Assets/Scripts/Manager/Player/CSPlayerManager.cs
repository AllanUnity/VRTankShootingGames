using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSPlayerManager : CSMonoSingleton<CSPlayerManager>
{
    public override void Init()
    {
        base.Init();
        InitPlayer();
    }
    
    private TKController tkController;
    private RenderTexture tkSightTexture;
    /// <summary>初始化角色</summary>
    private void InitPlayer()
    {
        GameObject Player_Abrams = Instantiate(CSGame.Instance.GetStaticObj("Player_Abrams"));
        Player_Abrams.transform.position = Vector3.zero;
        tkController = Player_Abrams.GetComponent<TKController>();
        tkController.Init();

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
        RotateTower(vec.x, vec.y);
    }
    /// <summary>旋转</summary>
    public void RotateTower(float x, float y)
    {
        tkController.towerController.RotateTower(x, y);
    }

    /// <summary>开火</summary>
    public void Fire()
    {
        tkController.fireController.Fire();
    }
}

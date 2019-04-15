using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>场景管理器</summary>
public class CSScenesManager : CSMonoSingleton<CSScenesManager>
{
    public override void Init()
    {
        base.Init();

    }
    /// <summary>当前的场景</summary>
    public ESceneType CurrentSceneType = ESceneType.Start;
    /// <summary>将要加载的下一个场景</summary>
    public ESceneType NextSceneType=ESceneType.Main;

    /// <summary>加载场景</summary>
    /*
     首先判断是否和当前场景相同, 如果相同则不处理---不能重复加载当前场景
     其次判断是否和下一个场景相同, 如果相同则不处理---此时已经进入下一个场景加载了

     无论场景为那一个场景, 都要进入Loading场景,Start和Loading时单独做判断,不进行异步加载, 直接跳转, Main和Combat需要进行数据清除

         */
    public void LoadScene(ESceneType type)
    {
        if (type != CurrentSceneType || type != NextSceneType)
        {
            NextSceneType = type;
            UIManager.Singleton.CloseAll();
            switch (type)
            {
                case ESceneType.Start:
                    /*
                     * 
                     */
                    CurrentSceneType = type;
                    //UIManager.Singleton.OpenPanel<>
                    break;
                case ESceneType.Main:
                    /*

                     */
                    break;
                case ESceneType.Loading:

                    break;
                case ESceneType.Combat:

                    break;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSGame : MonoBehaviour
{
    #region 单例
    private static CSGame instance;

    public static CSGame Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    #region 字段
    /// <summary>是否输出日志</summary>
    public bool IsDebugLog = true;
    /// <summary>是否引用本地资源</summary>
    private bool isLoadLocalRex = false;
    public bool IsLoadLocalRex { get { return isLoadLocalRex; } }

    /// <summary>版本号</summary>
    public string localVersion = "1.0.0";

    /// <summary>当前游戏场景</summary>
    private CGameState currentState;
    public CGameState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    /// <summary>上一个游戏场景</summary>
    private CGameState lastState;
    public CGameState LastState
    {
        get { return lastState; }
        set { lastState = value; }
    }
    /// <summary>下一个场景</summary>
    private CGameState nextState;
    public CGameState NextState
    {
        get { return nextState; }
        set { nextState = value; }
    }
    /// <summary>屏幕尺寸</summary>
    [SerializeField]
    private Vector2 sceneSize;
    public Vector2 SceneSize { get { return sceneSize; } }

    /// <summary>主UI</summary>
    public Transform MainCanvas;
    #endregion
    /// <summary>常驻资源</summary>
    [Serializable]
    public class ResidentAssets
    {
        public string Name;
        public GameObject games;
    }
    public ResidentAssets[] residentAssets;

    /// <summary>事件</summary>
    public EventHanlderManager EventHanlder = new EventHanlderManager();
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        Debug.developerConsoleVisible = IsDebugLog;
        GameObject.DontDestroyOnLoad(this);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        Application.targetFrameRate = 45;

        InitMonoManager();
    }
    private void InitMonoManager()
    {
        UILayerManager.Instance.Init();
        CSExceptionManager.Instance.Init();
    }
}

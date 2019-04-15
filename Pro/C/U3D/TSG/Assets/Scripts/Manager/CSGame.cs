using System.Collections.Generic;
using UnityEngine;

public class CSGame : MonoBehaviour
{
    private static CSGame instance;
    public static CSGame Instance { get { return instance; } }

    /// <summary>屏幕尺寸</summary>
    [SerializeField]
    private Vector2 sceneSize = new Vector2(1920, 1080);
    /// <summary>屏幕尺寸</summary>
    public Vector2 SceneSize { get { return sceneSize; } }

    #region 资源
    /// <summary>主UI</summary>
    public Transform MainCanvas;
    /// <summary>主相机</summary>
    public Camera MainCamera;
    /// <summary>主灯光</summary>
    public Light DirectionalLight;
    /// <summary>常驻资源管理类</summary>
    [System.Serializable]
    public struct ResidentAssets
    {
        public string typeName;
        public GameObject GamePrefab;
    }

    /// <summary>常驻资源</summary>
    [SerializeField]
    private List<ResidentAssets> residentAssets = new List<ResidentAssets>();
    /// <summary>获取常驻资源</summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public GameObject GetStaticObj(string typeName)
    {
        for (int i = 0; i < residentAssets.Count; i++)
        {
            ResidentAssets resident = residentAssets[i];
            if (resident.typeName == typeName)
            {
                return resident.GamePrefab;
            }
        }
        return null;
    }
    #endregion
    /// <summary>事件</summary>
    public EventHanlderManager EventHanlder = new EventHanlderManager();

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        Debug.LogColor("开始初始化");
        GameObject.DontDestroyOnLoad(this);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Debug.LogColor("设置不休眠");
        instance = this;
        Debug.LogColor("设置Game单例");
        Screen.SetResolution((int)SceneSize.x, (int)SceneSize.y, false);
        Debug.LogColor("设置分辨率");

        Application.targetFrameRate = 45;
        Debug.LogColor("设置帧率为" + Application.targetFrameRate);
        InitAssets();
        InitMonoManager();
        FinishInit();
    }
    #region 初始化资源
    /// <summary>初始化资源管理</summary>
    private void InitAssets()
    {
        InitMainCanvas();
        InitMainCamera();
        InitLight();
    }
    private void InitMainCanvas()
    {
        Debug.LogColor("创建主UI");
        if (MainCanvas == null)
        {
            GameObject canvasGames = GameObject.Find("UI");
            if (canvasGames != null)
            {
                MainCanvas = canvasGames.transform.Find("MainUI/MainCanvas");
                return;
            }

            GameObject UIPrefab = GetStaticObj("UI");
            if (UIPrefab == null)
            {
                Debug.LogError("主UI报空,请在GameManager中绑定UI");
                return;
            }
            GameObject UI = Instantiate(UIPrefab);
            UI.name = "UI";
            MainCanvas = UI.transform.Find("MainUI/MainCanvas");
        }
    }
    /// <summary>初始化主相机</summary>
    private void InitMainCamera()
    {
        Debug.LogColor("创建主相机");
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
            if (MainCamera == null)
            {
                GameObject CameraPrefab = GetStaticObj("Main Camera");
                if (CameraPrefab == null)
                {
                    Debug.LogError("主相机报空,请在GameManager中绑定主相机");
                }
                GameObject cameraGame = Instantiate(CameraPrefab);
                cameraGame.name = "Main Camera";
                Transform cameraTrans = cameraGame.transform;
                cameraTrans.position = new Vector3(0, 1, -10);
                cameraTrans.eulerAngles = Vector3.zero;
                cameraTrans.localScale = Vector3.one;

                MainCamera = cameraGame.GetComponent<Camera>();
            }
        }
    }
    /// <summary>初始化灯光</summary>
    private void InitLight()
    {
        Debug.LogColor("创建主灯光");
        if (DirectionalLight == null)
        {

            GameObject LightPrefab = GetStaticObj("Directional Light");
            if (LightPrefab == null)
            {
                Debug.LogError("主相机报空,请在GameManager中绑定主相机");
            }
            GameObject LightGame = Instantiate(LightPrefab);
            LightGame.name = "Directional Light";
            Transform LightTrans = LightGame.transform;
            LightTrans.position = new Vector3(0, 1, -10);
            LightTrans.eulerAngles = new Vector3(50, 330, 0);
            LightTrans.localScale = Vector3.one;

            DirectionalLight = LightGame.GetComponent<Light>();

        }
    }
    #endregion

    #region 初始化管理类
    /// <summary>初始化管理类</summary>
    private void InitMonoManager()
    {
        Debug.LogColor("初始化管理类");
        InputManager.Initialize(transform);//输入
        CSTimeManager.Initialize(transform);//时间
        CSToolsManager.Initialize(transform);//工具
        CSQRCoderManager.Initialize(transform);//二维码
        CSResourceManager.Initialize(transform);//资源
        CSScenesManager.Initialize(transform);//场景管理器

        UILayerManager.Initialize(transform);//UI层级
        UIManager.Initialize(transform);//UI管理
        CSTipsManager.Initialize(transform);//提示层

        CSNetManager.Initialize(transform);//网络

    }
    #endregion

    private void FinishInit()
    {
        InputManager.Add(KeyCode.Escape, () =>
        {
            if (!UIManager.Singleton.IsOpenPanel<UIESCPanel>())
                UIManager.Singleton.OpenPanel<UIESCPanel>();
            else
            {
                UIManager.Singleton.ClosePanel<UIESCPanel>();
            }
        });
        CSScenesManager.Singleton.LoadScene(ESceneType.Main);
    }

    private void FixedUpdate()
    {
        CSTimeManager.Singleton.OnFixedUpdate();
    }
    private void Update()
    {
        CSTimeManager.Singleton.OnUpdate(Time.deltaTime);
    }

    public void Quit()
    {
        UIManager.Singleton.CloseAll();
#if UNITY_EDITOR
        Debug.Log("结束游戏");
#else
        Application.Quit();
#endif
    }
}

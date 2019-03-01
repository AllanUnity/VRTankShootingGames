using System.Collections.Generic;
using UnityEngine;

public class CSGame : MonoBehaviour
{
    private static CSGame instance;
    public static CSGame Instance { get { return instance; } }

    /// <summary>屏幕尺寸</summary>
    [SerializeField]
    private Vector2 sceneSize;
    /// <summary>屏幕尺寸</summary>
    public Vector2 SceneSize { get { return sceneSize; } }

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

    /// <summary>事件</summary>
    public EventHanlderManager EventHanlder = new EventHanlderManager();

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        GameObject.DontDestroyOnLoad(this);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        Screen.SetResolution((int)SceneSize.x, (int)SceneSize.y, false);

        Application.targetFrameRate = 45;

        InitAssets();
        InitMonoManager();
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
        if (MainCanvas == null)
        {
            GameObject canvasGames = GameObject.Find("UI");
            if (canvasGames!=null)
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
            MainCanvas = UI.transform.Find("MainUI/MainCanvas");
        }
    }
    /// <summary>初始化主相机</summary>
    private void InitMainCamera()
    {
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
        if (DirectionalLight == null)
        {

            GameObject LightPrefab = GetStaticObj("Directional Light");
            if (LightPrefab == null)
            {
                Debug.LogError("主相机报空,请在GameManager中绑定主相机");
            }
            GameObject LightGame = Instantiate(LightPrefab);
            Transform LightTrans = LightGame.transform;
            LightTrans.position = new Vector3(0, 1, -10);
            LightTrans.eulerAngles = new Vector3(50, 330, 0);
            LightTrans.localScale = Vector3.one;

            DirectionalLight = LightGame.GetComponent<Light>();

        }
    }
    #endregion
    List<IOnUpdate> updates = new List<IOnUpdate>();
    public void AddUpdate(IOnUpdate message)
    {
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i]==message)
            {
                return;
            }
        }
        updates.Add(message);
    }
    /// <summary>初始化管理类</summary>
    private void InitMonoManager()
    {
        UILayerManager.Initialize(transform);
        CSExceptionManager.Initialize(transform);
        UIManager.Initialize(transform);
        CSTimeManager.Initialize(transform);

        CSPlayerManager.Initialize(transform);
    }



    private void FixedUpdate()
    {
        OnUpdate();
    }
    private void OnUpdate()
    {
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i]==null)
            {
                updates.RemoveAt(i);
                continue;
            }
            updates[i].OnUpdate();
        }
    }
    private void Quit()
    {
        Application.Quit();
    }
}

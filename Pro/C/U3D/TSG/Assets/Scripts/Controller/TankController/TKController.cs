using UnityEngine;

/// <summary>坦克控制器</summary>
public class TKController : MonoBehaviour, IOnUpdate
{
    public TKAbramsFireController fireController;
    public TKTowerController towerController;

    public RenderTexture sightTexture;
    public Camera sightCamera;
    // Start is called before the first frame update
    public void Init()
    {
        sightTexture = new RenderTexture(500, 500, 100);
        sightCamera.targetTexture = sightTexture;
        towerController.Init();
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        towerController.OnUpdate();
    }
}

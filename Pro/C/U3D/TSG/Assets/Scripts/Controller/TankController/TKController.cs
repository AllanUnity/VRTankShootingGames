using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKController : MonoBehaviour
{
    public TKAbramsFireController fireController;
    public TKTowerController towerController;

    public RenderTexture sightTexture;
    public Camera sightCamera;
    // Start is called before the first frame update
    public void Init()
    {
        sightTexture = new RenderTexture(500,500,100);
        sightCamera.targetTexture = sightTexture;
    }

}

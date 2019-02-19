using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKTowerModelBase : MonoBehaviour
{
    public TKTowerControllerBase towerControllerBase;

    public float towerTarget;
    public float gunTarget;

    /// <summary>旋转炮塔</summary>
    public void RotateTower()
    {
        
    }

    public void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        towerControllerBase.SetTargetVector(towerTarget, gunTarget);
    }
}

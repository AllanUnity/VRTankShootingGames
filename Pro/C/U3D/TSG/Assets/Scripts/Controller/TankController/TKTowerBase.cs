using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKTowerBase : MonoBehaviour
{
    public Vector2 targetVector;
    public Transform TowerTrans;
    public Transform GunTrans;

    private void Update()
    {
        SetTargetVector(targetVector);
    }

    public void SetTargetVector(Vector2 targetVector)
    {
        this.targetVector = targetVector;
        SetTowerAndGun();
    }
    public void SetTowerAndGun()
    {
        TowerTrans.transform.localEulerAngles = new Vector3(0, targetVector.y, 0);
        GunTrans.transform.localEulerAngles = new Vector3(targetVector.x, 0, 0);
    }
}

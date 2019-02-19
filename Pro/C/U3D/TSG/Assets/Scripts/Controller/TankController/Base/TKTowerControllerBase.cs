using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKTowerControllerBase : MonoBehaviour
{

    /// <summary>炮塔</summary>
    [SerializeField]
    protected Transform TowerTrans;
    /// <summary>炮管</summary>
    [SerializeField]
    protected Transform GunTrans;

    private float torwerTargetAngles;
    private float gunTargetAngles;


    public void SetTargetVector(float tower,float gun)
    {
        this.torwerTargetAngles = tower;
        this.gunTargetAngles = gun;
        SetTowerAndGun();
    }
    public void SetTowerAndGun()
    {
        TowerTrans.transform.localEulerAngles = new Vector3(0, torwerTargetAngles, 0);
        GunTrans.transform.localEulerAngles = new Vector3(gunTargetAngles, 0, 0);
    }
}

using UnityEngine;

public class TKTowerController : MonoBehaviour
{
    /// <summary>炮塔</summary>
    [SerializeField]
    protected Transform TowerTrans;
    /// <summary>炮管</summary>
    [SerializeField]
    protected Transform GunTrans;


    /// <summary>炮塔的目标角度</summary>
    public float towerTargetAngles;
    /// <summary>炮管的目标角度</summary>
    public float gunTargetAngles;

    /// <summary>当前的炮塔的角度</summary>
    private float currentTowerAngles;
    /// <summary>当前的炮管的角度</summary>
    private float currentGunAngles;

    /// <summary>水平旋转速度</summary>
    public float horizontalRotateSpeed;
    /// <summary>竖直旋转速度</summary>
    public float verticalRotateSpeed;

    /// <summary>旋转方向</summary>
    public float rotateHorizontalType;
    /// <summary>旋转增量</summary>
    public float rotateVerticalType;


    #region MonoFunction
    public void Update()
    {
        ChangeTargetAngles();
        RotateCurrentTowerAndGun();
        SetTowerAndGun();
    }

    /// <summary>自增目标角度并做限制</summary>
    public virtual void ChangeTargetAngles()
    {
        towerTargetAngles += (Time.deltaTime * horizontalRotateSpeed * rotateHorizontalType);
        if (towerTargetAngles > 360)
        {
            towerTargetAngles -= 360;
            currentTowerAngles -= 360;
        }

        gunTargetAngles -= (Time.deltaTime * verticalRotateSpeed * rotateVerticalType);
        if (gunTargetAngles < 0)
        {
            gunTargetAngles += 360;
            currentGunAngles += 360;
        }
    }
    /// <summary>计算旋转炮塔和炮管的角度</summary>
    public virtual void RotateCurrentTowerAndGun()
    {
        if (currentTowerAngles < towerTargetAngles) { currentTowerAngles = currentTowerAngles + (horizontalRotateSpeed * Time.deltaTime); }
        if (currentTowerAngles > towerTargetAngles) { currentTowerAngles = currentTowerAngles - (horizontalRotateSpeed * Time.deltaTime); }

        if (currentGunAngles < gunTargetAngles) { currentGunAngles = currentGunAngles + (verticalRotateSpeed * Time.deltaTime); }
        if (currentGunAngles > gunTargetAngles) { currentGunAngles = currentGunAngles - (verticalRotateSpeed * Time.deltaTime); }
    }

    /// <summary>
    /// 设置角度
    /// </summary>
    public void SetTowerAndGun()
    {
        TowerTrans.transform.localEulerAngles = new Vector3(0, currentTowerAngles, 0);
        GunTrans.transform.localEulerAngles = new Vector3(currentGunAngles, 0, 0);
    }
    #endregion

    /// <summary>旋转炮塔</summary>
    public void RotateTower(Vector2 vector)
    {
        float tower = vector.x;
        float gun = vector.y;
        if (tower >= 180)
        {
            do
            {
                tower -= 180;
            } while (tower >= 180);
        }
        else if (tower <= -180)
        {
            do
            {
                tower += 180;
            } while (tower <= -180);
        }
        if (gun >= 180)
        {
            do
            {
                gun -= 180;
            } while (gun >= 180);
        }
        else if (gun <= -180)
        {
            do
            {
                gun += 180;
            } while (gun <= -180);
        }
        towerTargetAngles = tower;
        gunTargetAngles = gun;
    }
    /// <summary>炮塔旋转</summary>
    public void RotateTower(float horizontalType, float verticalType)
    {
        rotateHorizontalType = horizontalType;
        rotateVerticalType = verticalType;
    }

}

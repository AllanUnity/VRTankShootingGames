using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKTowerModelBase : MonoBehaviour
{
    public TKTowerControllerBase towerControllerBase;

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
    public RotateType rotateHorizontalType;
    public RotateType rotateVerticalType;

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
    public void RotateTower(RotateType horizontalType, RotateType verticalType)
    {
        rotateHorizontalType = horizontalType;
        rotateVerticalType = verticalType;
    }

    public void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        ChangeTargetAngles();
        RotateCurrentTowerAndGun();
    }

    public virtual void ChangeTargetAngles()
    {
        switch (rotateHorizontalType)
        {
            case RotateType.Left:
                {
                    towerTargetAngles += (Time.deltaTime * horizontalRotateSpeed);
                    if (towerTargetAngles > 360)
                    {
                        towerTargetAngles -= 360;
                        currentTowerAngles -= 360;
                    }
                }
                break;
            case RotateType.Right:
                {
                    towerTargetAngles -= (Time.deltaTime * horizontalRotateSpeed);
                    if (towerTargetAngles < 0)
                    {
                        towerTargetAngles += 360;
                        currentTowerAngles += 360;
                    }
                }
                break;
        }
        switch (rotateVerticalType)
        {
            case RotateType.Down:
                {
                    gunTargetAngles -= (Time.deltaTime * verticalRotateSpeed);
                    if (gunTargetAngles < 0)
                    {
                        gunTargetAngles += 360;
                        currentGunAngles += 360;
                    }
                }
                break;
            case RotateType.Up:
                {
                    gunTargetAngles += (Time.deltaTime * verticalRotateSpeed);
                    if (gunTargetAngles > 0)
                    {
                        gunTargetAngles -= 360;
                        currentGunAngles -= 360;
                    }
                }
                break;
        }
    }
    /// <summary>旋转炮塔和炮管</summary>
    public virtual void RotateCurrentTowerAndGun()
    {
        if (currentTowerAngles < towerTargetAngles)
        {
            currentTowerAngles = currentTowerAngles + (horizontalRotateSpeed * Time.deltaTime);
        }
        if (currentTowerAngles > towerTargetAngles)
        {
            currentTowerAngles = currentTowerAngles - (horizontalRotateSpeed * Time.deltaTime);
        }

        if (currentGunAngles < gunTargetAngles)
        {
            currentGunAngles = currentGunAngles + (verticalRotateSpeed * Time.deltaTime);
        }
        if (currentGunAngles > gunTargetAngles)
        {
            currentGunAngles = currentGunAngles - (verticalRotateSpeed * Time.deltaTime);
        }

        towerControllerBase.SetTargetVector(currentTowerAngles, currentGunAngles);
    }
}

public enum RotateType
{
    Stop,
    Left,
    Right,
    Up,
    Down
}
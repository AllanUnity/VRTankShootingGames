using System;
using UnityEngine;

/// <summary>炮塔控制器</summary>
public class TKTowerController : MonoBehaviour
{
    /// <summary>炮塔</summary>
    public Transform TowerTrans;
    /// <summary>炮管</summary>
    public Transform GunTrans;

    /// <summary>炮塔的目标角度</summary>
    /*
     * 正前方为0度.左后各180度为一圈 
     * 极限值为-180~180时可无限旋转. 其他时为限制性
     * 
     */
    public float targetX_Angles;
    /// <summary>炮管的目标角度</summary>
    public float targetY_Angles;

    /// <summary>当前的炮塔的角度</summary>
    public float currentX_Angles;
    /// <summary>当前的炮管的角度</summary>
    public float currentY_Angles;

    /// <summary>水平旋转速度</summary>
    public float xRotateSpeed;
    /// <summary>竖直旋转速度</summary>
    public float yRotateSpeed;

    /// <summary>水平旋转增量</summary>
    public float rotateHorizontalType;
    /// <summary>竖直旋转增量</summary>
    public float rotateVerticalType;

    /// <summary>X轴的最小值</summary>
    public float xMinRestrict;
    /// <summary>X轴的最大值</summary>
    public float xMaxRestrict;

    public bool isTotalX;

    /// <summary>俯角的最低限制曲线</summary>
    public AnimationCurve yMinRestrict;
    /// <summary>仰角的最大限制曲线</summary>
    public AnimationCurve yMaxRestrict;

    public void Init()
    {
        if (yMinRestrict.keys[0].time == yMaxRestrict.keys[0].time)
            xMinRestrict = yMinRestrict[0].time;
        else
            Debug.LogError("旋转角度设置错误, 请重新设置");

        if (yMinRestrict.keys[yMinRestrict.length - 1].time == yMaxRestrict.keys[yMaxRestrict.length - 1].time)
            xMaxRestrict = yMinRestrict[yMinRestrict.length - 1].time;
        else
            Debug.LogError("旋转角度设置错误, 请重新设置");

        if (xMinRestrict == -180 && xMaxRestrict == 180)
        {
            isTotalX = true;
        }
    }
    /// <summary>设置横向和纵向增量</summary>
    public void RotateTower(float horizontalType, float verticalType)
    {
        rotateHorizontalType = horizontalType;
        rotateVerticalType = -verticalType;
    }
    public void OnUpdate()
    {
        ChangeTargetAngles();
        RotateCurrentTowerAndGun();
        SetTowerAndGun();
    }
    /// <summary>自增目标角度并做限制</summary>
    public virtual void ChangeTargetAngles()
    {
        #region X轴
        targetX_Angles += (Time.deltaTime * xRotateSpeed * rotateHorizontalType);
        if (!isTotalX)
        {
            #region 非全向
            if (targetX_Angles < xMinRestrict)
            {
                targetX_Angles = xMinRestrict;
            }
            else if (targetX_Angles > xMaxRestrict)
            {
                targetX_Angles = xMaxRestrict;
            }
            #endregion
        }
        else
        {
            if (targetX_Angles >= 180)
            {
                targetX_Angles -= 360;
            }
            if (targetX_Angles <= -180)
            {
                targetX_Angles += 360;
            }
        }
        #endregion
        #region Y轴
        targetY_Angles += (Time.deltaTime * yRotateSpeed * rotateVerticalType);
        if (targetY_Angles < yMaxRestrict.Evaluate(currentX_Angles))
        {
            targetY_Angles = yMaxRestrict.Evaluate(currentX_Angles);
        }
        if (targetY_Angles > yMinRestrict.Evaluate(currentX_Angles))
        {
            targetY_Angles = yMinRestrict.Evaluate(currentX_Angles);
        }
        #endregion
    }
    /// <summary>计算旋转炮塔和炮管的角度</summary>
    public virtual void RotateCurrentTowerAndGun()
    {
        #region X轴
        if (!isTotalX)
        {
            #region 非全向
            if (currentX_Angles < targetX_Angles)
            {
                currentX_Angles = currentX_Angles + (xRotateSpeed * Time.deltaTime);
            }
            if (currentX_Angles > targetX_Angles)
            {
                currentX_Angles = currentX_Angles - (xRotateSpeed * Time.deltaTime);
            }
            #endregion
        }
        else
        {
            if (currentX_Angles < targetX_Angles)
            {

                if (currentX_Angles - targetX_Angles < 180)
                {
                    currentX_Angles = currentX_Angles + (xRotateSpeed * Time.deltaTime);
                }
                else
                {
                    currentX_Angles = currentX_Angles - (xRotateSpeed * Time.deltaTime);
                }
            }
            if (currentX_Angles > targetX_Angles)
            {
                if (currentX_Angles - targetX_Angles < 180)
                {
                    currentX_Angles = currentX_Angles - (xRotateSpeed * Time.deltaTime);
                }
                else
                {
                    currentX_Angles = currentX_Angles + (xRotateSpeed * Time.deltaTime);
                }
            }
            if (currentX_Angles >= 180)
            {
                currentX_Angles -= 360;
            }
            if (currentX_Angles <= -180)
            {
                currentX_Angles += 360;
            }
        }
        #endregion

        #region Y轴

        if (currentY_Angles < targetY_Angles)
        {
            currentY_Angles = currentY_Angles + (yRotateSpeed * Time.deltaTime);
        }
        if (currentY_Angles > targetY_Angles)
        {
            currentY_Angles = currentY_Angles - (yRotateSpeed * Time.deltaTime);
        }
        #endregion
    }

    /// <summary>
    /// 设置角度
    /// </summary>
    public void SetTowerAndGun()
    {
        TowerTrans.transform.localEulerAngles = new Vector3(0, currentX_Angles, 0);
        GunTrans.transform.localEulerAngles = new Vector3(-currentY_Angles, 0, 0);
    }
}

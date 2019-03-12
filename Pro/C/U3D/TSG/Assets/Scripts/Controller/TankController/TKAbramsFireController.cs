﻿using UnityEngine;

/// <summary>发射单元</summary>
public class TKAbramsFireController : MonoBehaviour
{
    /*
     炮弹发射系统:
            分为备选炮弹和待击炮弹,选定待击炮弹后,开始装填待击炮弹.待击炮弹开始倒计时,倒计时结束后方可发射.
            备选炮弹至少有一种
            公开功能分为发射和选定待击炮弹
            公开接口为待击炮弹种类,待击炮弹上弹倒计时,当前准备好的炮弹, 是否准备好
         */
    /// <summary>备选炮弹</summary>
    public GameObject[] alternativeBullet;
    /// <summary>下一发炮弹</summary>
    public GameObject nextBullet;
    /// <summary>待发射炮弹</summary>
    public GameObject bulletPrefab;
    /// <summary>发射位置</summary>
    public GameObject firePosition;


    /// <summary>预备下一发炮弹</summary>
    public void ReadyNextBullet(BulletType bt)
    {
        if (alternativeBullet.Length <= 0)
        {
            Debug.Log("发射单元未绑定默认炮弹");
            return;
        }
        for (int i = 0; i < alternativeBullet.Length; i++)
        {
            if (alternativeBullet[i] == null)
            {
                Debug.Log("发射器单元绑定默认炮弹为空");
                return;
            }
        }
        switch (bt)
        {
            case BulletType.Default0:
                {
                    nextBullet = alternativeBullet[0];
                }
                break;
            case BulletType.Default1:
                {
                    if (alternativeBullet.Length >= 1)
                    {
                        nextBullet = alternativeBullet[1];
                    }
                }
                break;
            case BulletType.Default2:
                {
                    if (alternativeBullet.Length >= 2)
                    {
                        nextBullet = alternativeBullet[2];
                    }
                }
                break;
            default:
                {
                    nextBullet = alternativeBullet[0];
                }
                break;
        }
    }
    /// <summary>开火</summary>
    public void Fire()
    {
        if (TimeRemaining > 0)
        {
            Debug.Log("炮弹未装填完毕!!!");
            return;
        }
        GameObject _bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
        _bullet.GetComponent<TKBullet>().FireInit(this);
    }
    /// <summary>装填时间</summary>
    private float cdTime = 2;
    /// <summary>剩余装填时间</summary>
    private float TimeRemaining = 0;
    /// <summary>倒计时</summary>
    private void StartLoadingShells()
    {

    }

}


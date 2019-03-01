using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKAbramsFireController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePosition;

    public void Fire(BulletType bt)
    {
        GameObject _bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
        _bullet.GetComponent<TKBullet>().FireInit(this);
    }

}


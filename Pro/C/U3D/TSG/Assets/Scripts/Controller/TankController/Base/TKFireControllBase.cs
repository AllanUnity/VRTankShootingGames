using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKFireControllBase : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePosition;

    public void Fire()
    {
        GameObject _bullet = Instantiate(bulletPrefab, firePosition.transform.position, firePosition.transform.rotation);
        Rigidbody _rigidbody = _bullet.AddComponent<Rigidbody>();
        //_rigidbody.AddRelativeForce(Vector3.forward*10, ForceMode.Impulse);
    }


}

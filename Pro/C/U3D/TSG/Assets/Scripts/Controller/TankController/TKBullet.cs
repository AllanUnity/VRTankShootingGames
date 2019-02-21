using UnityEngine;

public class TKBullet : MonoBehaviour, IOnUpdate
{

    public GameObject bulletTrans;
    /// <summary>飞行速度</summary>
    public float bulletSpeed;
    /// <summary>发射者</summary>
    private TKAbramsFireController fireTrans;
    /// <summary>发射点的位置</summary>
    private Vector3 firePostion;
    /// <summary>炮弹开始飞行的时间</summary>
    private float fireStartTime;
    public void FireInit(TKAbramsFireController fireTrans)
    {
        this.fireTrans = fireTrans;
        Rigidbody _rigidbody = bulletTrans.AddComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.forward * bulletSpeed;
        fireStartTime = Time.time;
    }

    public virtual void OnUpdate()
    {
        Reduction();
    }

    /// <summary>命中</summary>
    private void Accuracy()
    {

    }
    /// <summary>减速</summary>
    private void Reduction()
    {

    }

}

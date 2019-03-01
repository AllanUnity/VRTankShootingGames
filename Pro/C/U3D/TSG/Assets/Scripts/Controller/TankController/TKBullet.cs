using UnityEngine;

/// <summary>坦克炮弹</summary>
public class TKBullet : MonoBehaviour
{

    public GameObject bulletTrans;
    /// <summary>飞行速度</summary>
    public float bulletSpeed;

    /// <summary>最大飞行距离</summary>
    public float maxDistance;

    /// <summary>发射者</summary>
    private TKAbramsFireController fireTrans;
    /// <summary>发射点的位置</summary>
    private Vector3 firePostion=Vector3.one;
    /// <summary>炮弹开始飞行的时间</summary>
    private float fireStartTime;


    public void FireInit(TKAbramsFireController fireTrans)
    {
        this.fireTrans = fireTrans;
        transform.localScale = Vector3.one;
        Rigidbody _rigidbody = bulletTrans.AddComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * bulletSpeed;
        fireStartTime = Time.time;
    }
    private void FixedUpdate()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        DetectionDistance(transform.position);
        Reduction();
        Detection();
    }
    /// <summary>检测最大距离</summary>
    private bool DetectionDistance(Vector3 point)
    {
        if (Vector3.Distance(point, firePostion) > maxDistance)
        {
            Debug.Log("超出最大距离");
            Destroy(gameObject);
            return false;
        }
        return true;
    }
    /// <summary>主动检测目标</summary>
    private void Detection()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.distance);
            if (hit.distance < 10)
            {
                Accuracy(hit);
            }
        }
    }
    /// <summary>命中</summary>
    private void Accuracy(RaycastHit hit)
    {
        if (!DetectionDistance(hit.point))
        {
            return;
        }

        Debug.Log("fireTrans:命中物体");

        Destroy(gameObject);
    }
    /// <summary>减速</summary>
    private void Reduction()
    {

    }

}

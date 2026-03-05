using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float step = speed * Time.deltaTime;

        if (dir.magnitude <= step)
        {
            Hit();
            return;
        }

        transform.position += dir.normalized * step;
    }

    void Hit()
    {
        var hp = target.GetComponent<EnemyHealth>();
        if (hp != null) hp.TakeDamage(damage);
        Destroy(gameObject);
    }
}
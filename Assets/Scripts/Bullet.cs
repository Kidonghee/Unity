using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    public bool splash = false;
    public float splashRadius = 1.2f;

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
        if (!splash)
        {
            EnemyHealth hp = target.GetComponent<EnemyHealth>();
            if (hp != null) hp.TakeDamage(damage);
        }
        else
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(target.position, splashRadius);

            foreach (var h in hits)
            {
                EnemyHealth hp = h.GetComponent<EnemyHealth>();
                if (hp != null) hp.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
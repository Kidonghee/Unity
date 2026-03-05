using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float range = 3.5f;
    public float fireRate = 1.0f; // 초당 발사(1 = 1초에 1발)
    public GameObject bulletPrefab;

    float cooldown = 0f;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown > 0f) return;

        Transform target = FindNearestEnemy();
        if (target == null) return;

        Shoot(target);
        cooldown = 1f / fireRate;
    }

    Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        if (hits.Length == 0) return null;

        Transform best = null;
        float bestDist = float.MaxValue;

        foreach (var h in hits)
        {
            float d = (h.transform.position - transform.position).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = h.transform;
            }
        }
        return best;
    }

    void Shoot(Transform target)
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().SetTarget(target);
    }

    // 범위 시각화(씬에서만)
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
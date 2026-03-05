using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    public LayerMask enemyLayer;

    public float range = 3.5f;
    public float fireRate = 1f;

    public GameObject bulletPrefab;

    public int damage = 1;

    // Splash 옵션
    public bool useSplash = false;
    public float splashRadius = 1.2f;

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

        float bestDist = float.MaxValue;
        Transform bestTarget = null;

        foreach (var h in hits)
        {
            float d = (h.transform.position - transform.position).sqrMagnitude;

            if (d < bestDist)
            {
                bestDist = d;
                bestTarget = h.transform;
            }
        }

        return bestTarget;
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null) return;

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Bullet bullet = b.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.damage = damage;
            bullet.splash = useSplash;
            bullet.splashRadius = splashRadius;

            bullet.SetTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
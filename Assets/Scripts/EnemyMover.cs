using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.5f;
    public int damageToBase = 1;

    int index = 0;

    float slowMultiplier = 1f;
    float slowTimer = 0f;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
            {
                slowMultiplier = 1f;
            }
        }

        Transform target = waypoints[index];
        Vector3 dir = target.position - transform.position;

        float currentSpeed = speed * slowMultiplier;
        float step = currentSpeed * Time.deltaTime;

        if (dir.magnitude <= step)
        {
            transform.position = target.position;
            index++;

            if (index >= waypoints.Length)
            {
                ReachGoal();
                return;
            }
        }
        else
        {
            transform.position += dir.normalized * step;
        }
    }

    public void ApplySlow(float multiplier, float duration)
    {
        slowMultiplier = multiplier;
        slowTimer = duration;
    }

    void ReachGoal()
    {
        if (BaseHealth.Instance != null)
            BaseHealth.Instance.TakeDamage(damageToBase);

        Destroy(gameObject);
    }
}
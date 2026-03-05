using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.5f;
    public int damageToBase = 1;

    int index = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[index];
        Vector3 dir = target.position - transform.position;

        float step = speed * Time.deltaTime;

        if (dir.magnitude <= step)
        {
            transform.position = target.position;
            index++;

            if (index >= waypoints.Length)
            {
                ReachGoal();
            }
        }
        else
        {
            transform.position += dir.normalized * step;
        }
    }

    void ReachGoal()
    {
        if (BaseHealth.Instance != null)
            BaseHealth.Instance.TakeDamage(damageToBase);

        Destroy(gameObject);
    }
}
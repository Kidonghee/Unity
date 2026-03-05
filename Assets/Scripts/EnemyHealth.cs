using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 5;
    public int reward = 5;

    int hp;

    void Awake() => hp = maxHp;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            if (MoneyManager.Instance != null)
                MoneyManager.Instance.Add(reward);

            Destroy(gameObject);
        }
    }
}
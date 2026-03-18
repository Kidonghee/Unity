using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 5;
    int currentHp;

    public Transform hpBar;   // Fill 연결
    public int reward = 5;

    Vector3 hpBarStartScale;
    Vector3 hpBarStartPos;

    void Awake()
    {
        currentHp = maxHp;

        if (hpBar != null)
        {
            hpBarStartScale = hpBar.localScale;
            hpBarStartPos = hpBar.localPosition;
        }
    }

    public void InitHp(int hp)
    {
        maxHp = hp;
        currentHp = hp;

        if (hpBar != null)
        {
            hpBar.localScale = hpBarStartScale;
            hpBar.localPosition = hpBarStartPos;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp < 0) currentHp = 0;

        if (hpBar != null)
        {
            float ratio = Mathf.Clamp01((float)currentHp / maxHp);

            hpBar.localScale = new Vector3(hpBarStartScale.x * ratio, hpBarStartScale.y, hpBarStartScale.z);

            // 왼쪽 고정처럼 보이게 위치도 보정
            hpBar.localPosition = new Vector3(
                hpBarStartPos.x - (hpBarStartScale.x * (1f - ratio)) * 0.5f,
                hpBarStartPos.y,
                hpBarStartPos.z
            );
        }

        if (currentHp <= 0)
            Die();
    }

    void Die()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.Add(reward);

        Destroy(gameObject);
    }
}
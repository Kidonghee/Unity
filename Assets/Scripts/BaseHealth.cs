using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static BaseHealth Instance { get; private set; }

    public int maxHp = 20;
    public int CurrentHp { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        CurrentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        CurrentHp -= amount;
        Debug.Log($"Base HP: {CurrentHp}/{maxHp}");

        if (CurrentHp <= 0)
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
    }
}
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static BaseHealth Instance { get; private set; }

    public int maxHp = 10;   // ← 여기 10으로 변경
    public int CurrentHp { get; private set; }

    public bool IsGameOver { get; private set; } = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        if (IsGameOver) return;

        CurrentHp -= amount;

        if (CurrentHp < 0)
            CurrentHp = 0;

        Debug.Log("Base HP Now: " + CurrentHp + "/" + maxHp);

        if (CurrentHp <= 0)
        {
            IsGameOver = true;
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
    }
}
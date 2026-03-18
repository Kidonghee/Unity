using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int startMoney = 100;
    public int Money { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Money = startMoney;
    }

    public bool Spend(int amount)
    {
        if (Money < amount)
        {
            Debug.Log("돈 부족! 현재 돈: " + Money);
            return false;
        }

        Money -= amount;
        Debug.Log("돈 사용: " + amount + " / 남은 돈: " + Money);
        return true;
    }

    public void Add(int amount)
    {
        Money += amount;
        Debug.Log("돈 획득: " + amount + " / 현재 돈: " + Money);
    }
}
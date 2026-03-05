using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int startMoney = 100;
    public int Money { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        Money = startMoney;
        Debug.Log("Money: " + Money);
    }

    public bool CanAfford(int cost) => Money >= cost;

    public bool Spend(int cost)
    {
        if (Money < cost) return false;
        Money -= cost;
        Debug.Log("Money: " + Money);
        return true;
    }

    public void Add(int amount)
    {
        Money += amount;
        Debug.Log("Money: " + Money);
    }
}
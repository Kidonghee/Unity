using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public int buildCost = 25;

    public int level = 1;
    public int maxLevel = 5;

    public int upgradeCost = 20;
    public float upgradeCostMultiplier = 1.5f;

    public int sellPercent = 70;

    public GameObject rangeIndicator;

    TowerShooter shooter;

    void Awake()
    {
        shooter = GetComponent<TowerShooter>();
    }

    public bool CanUpgrade()
    {
        return level < maxLevel;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public int GetSellValue()
    {
        int invested = buildCost;

        int cost = upgradeCost;
        for (int i = 1; i < level; i++)
        {
            invested += cost;
            cost = Mathf.RoundToInt(cost * upgradeCostMultiplier);
        }

        return Mathf.RoundToInt(invested * (sellPercent / 100f));
    }

    public bool TryUpgrade()
    {
        if (!CanUpgrade()) return false;

        int cost = GetUpgradeCost();
        if (MoneyManager.Instance != null && !MoneyManager.Instance.Spend(cost))
        {
            Debug.Log("업그레이드 돈 부족!");
            return false;
        }

        level++;

        if (shooter != null)
        {
            shooter.range += 0.6f;
            shooter.fireRate += 0.3f;
            shooter.damage += 1;
        }

        upgradeCost = Mathf.RoundToInt(upgradeCost * upgradeCostMultiplier);

        Debug.Log($"Tower upgraded! Level {level}");
        return true;
    }

    public void Sell()
    {
        int value = GetSellValue();

        if (MoneyManager.Instance != null)
            MoneyManager.Instance.Add(value);

        Destroy(gameObject);
    }
}
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public int buildCost = 25;

    public int level = 1;
    public int maxLevel = 5;

    public int upgradeCost = 20;
    public float upgradeCostMultiplier = 1.5f;

    public int sellPercent = 70; // % 환급

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
        // 지금까지 투자한 금액을 기반으로 환급(간단 버전)
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

        // 업그레이드 효과(취향대로 조정 가능)
        if (shooter != null)
        {
            shooter.range += 0.6f;
            shooter.fireRate += 0.3f;
            shooter.damage += 1;
        }

        // 다음 업그레이드 비용 증가
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
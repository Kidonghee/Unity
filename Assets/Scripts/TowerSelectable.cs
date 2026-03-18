using UnityEngine;

public class TowerSelectable : MonoBehaviour
{
    TowerStats stats;
    TowerShooter shooter;

    public BuildSpot ownerSpot;

    void Awake()
    {
        stats = GetComponent<TowerStats>();
        shooter = GetComponent<TowerShooter>();
    }

    void Start()
    {
        if (stats == null) return;

        if (stats.rangeIndicator != null)
        {
            stats.rangeIndicator.SetActive(false);
            UpdateRangeVisual();
        }
    }

    void OnMouseDown()
    {
        if (TowerSelectionUI.Instance != null)
        {
            TowerSelectionUI.Instance.SelectTower(this);
        }
    }

    public void ShowRange(bool show)
    {
        if (stats != null && stats.rangeIndicator != null)
        {
            if (show)
            {
                UpdateRangeVisual();
            }

            stats.rangeIndicator.SetActive(show);
        }
    }

    void UpdateRangeVisual()
    {
        if (stats == null || stats.rangeIndicator == null || shooter == null)
            return;

        float diameter = shooter.range * 2f;

        Vector3 parentScale = transform.lossyScale;

        float scaleX = diameter / parentScale.x;
        float scaleY = diameter / parentScale.y;

        stats.rangeIndicator.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    public int GetSellPrice()
    {
        if (stats == null) return 0;
        return stats.GetSellValue();
    }

    public void Sell()
    {
        if (TowerSelectionUI.Instance != null)
        {
            TowerSelectionUI.Instance.DeselectCurrentTower();
        }

        if (ownerSpot != null)
        {
            ownerSpot.ClearSpot();
        }

        if (stats != null)
        {
            stats.Sell();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
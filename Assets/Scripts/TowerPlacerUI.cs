using UnityEngine;

public class TowerPlacerUI : MonoBehaviour
{
    public void SelectBasic()
    {
        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.SelectBasic();
    }

    public void SelectSniper()
    {
        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.SelectSniper();
    }

    public void SelectSplash()
    {
        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.SelectSplash();
    }

    public void SelectSlow()
    {
        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.SelectSlow();
    }

    public void CancelPlacement()
    {
        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.CancelPlacement();
    }
}
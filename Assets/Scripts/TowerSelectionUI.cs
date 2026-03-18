using TMPro;
using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static TowerSelectionUI Instance { get; private set; }

    public GameObject panel;
    public TMP_Text sellText;

    TowerSelectable currentTower;
    bool justOpened = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    void Update()
    {
        if (justOpened && Input.GetMouseButtonUp(0))
        {
            justOpened = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectCurrentTower();
        }
    }

    public void SelectTower(TowerSelectable tower)
    {
        if (currentTower != null && currentTower != tower)
        {
            currentTower.ShowRange(false);
        }

        currentTower = tower;
        currentTower.ShowRange(true);

        if (panel != null)
            panel.SetActive(true);

        if (sellText != null)
            sellText.text = "Sell (" + currentTower.GetSellPrice() + "G)";

        // 방금 열린 직후 클릭은 무시
        justOpened = true;
    }

    public void OnClickSell()
    {
        if (justOpened) return;

        if (currentTower != null)
        {
            currentTower.Sell();
        }
    }

    public void DeselectCurrentTower()
    {
        if (currentTower != null)
        {
            currentTower.ShowRange(false);
            currentTower = null;
        }

        if (panel != null)
            panel.SetActive(false);

        justOpened = false;
    }
}
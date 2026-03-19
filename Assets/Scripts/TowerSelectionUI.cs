using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerSelectionUI : MonoBehaviour
{
    public static TowerSelectionUI Instance { get; private set; }

    public GameObject panel;
    public TMP_Text sellText;
    public Button sellButton;

    TowerSelectable currentTower;
    bool canSell = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (sellButton != null)
            sellButton.interactable = false;
    }

    public void SelectTower(TowerSelectable tower)
    {
        if (tower == null) return;

        if (currentTower != null && currentTower != tower)
            currentTower.ShowRange(false);

        currentTower = tower;
        currentTower.ShowRange(true);

        if (panel != null)
            panel.SetActive(true);

        if (sellText != null)
            sellText.text = "Sell (" + currentTower.GetSellPrice() + "G)";

        StopAllCoroutines();
        StartCoroutine(EnableSellAfterDelay());
    }

    IEnumerator EnableSellAfterDelay()
    {
        canSell = false;

        if (sellButton != null)
            sellButton.interactable = false;

        // 클릭 겹침 방지
        yield return new WaitForSeconds(0.15f);

        canSell = true;

        if (sellButton != null)
            sellButton.interactable = true;
    }

    public void OnClickSell()
    {
        Debug.Log("판매 버튼 클릭");

        if (!canSell) return;
        if (currentTower == null) return;

        currentTower.Sell();

        // 판매 후 패널/배치 상태 정리
        DeselectCurrentTower();

        if (TowerPlacer.Instance != null)
            TowerPlacer.Instance.CancelPlacement();
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

        canSell = false;

        if (sellButton != null)
            sellButton.interactable = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DeselectCurrentTower();
        }
    }
}
using TMPro;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector Instance { get; private set; }

    public TowerStats selected;
    public TMP_Text towerInfoText;

    void Awake() => Instance = this;

    void Update()
    {
        // 타워 선택
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 p = new Vector2(mousePos.x, mousePos.y);

            Collider2D col = Physics2D.OverlapPoint(p);
            if (col != null)
            {
                TowerStats ts = col.GetComponent<TowerStats>();
                if (ts != null) selected = ts;
            }
        }

        // 선택 없음
        if (selected == null)
        {
            if (towerInfoText != null) towerInfoText.text = "";
            return;
        }

        // UI 갱신
        if (towerInfoText != null)
        {
            string up = selected.CanUpgrade()
                ? $"U: Upgrade ({selected.GetUpgradeCost()})"
                : "U: MAX";

            string sell = $"X: Sell ({selected.GetSellValue()})";

            towerInfoText.text = $"Tower Lv {selected.level}\n{up}\n{sell}";
        }

        // U = 업그레이드
        if (Input.GetKeyDown(KeyCode.U))
            selected.TryUpgrade();

        // X = 판매
        if (Input.GetKeyDown(KeyCode.X))
        {
            selected.Sell();
            selected = null;
        }
    }
}
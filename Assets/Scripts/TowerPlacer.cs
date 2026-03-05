using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public int towerCost = 25;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (towerPrefab == null)
        {
            Debug.LogError("towerPrefab 비어있음!");
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p = new Vector2(mousePos.x, mousePos.y);

        Collider2D col = Physics2D.OverlapPoint(p);
        Debug.Log(col ? $"HIT: {col.name}" : "HIT: null");

        if (col == null) return;

        BuildSpot spot = col.GetComponent<BuildSpot>();
        Debug.Log(spot ? "BuildSpot OK" : "BuildSpot null");

        if (spot == null) return;
        if (spot.occupied) { Debug.Log("이미 설치됨"); return; }

        if (MoneyManager.Instance != null && !MoneyManager.Instance.Spend(towerCost))
        {
            Debug.Log("돈 부족!");
            return;
        }

        Instantiate(towerPrefab, spot.transform.position, Quaternion.identity);
        spot.occupied = true;
        Debug.Log("TOWER PLACED!");
    }
}
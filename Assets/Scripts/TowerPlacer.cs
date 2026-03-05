using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject basicTowerPrefab;
    public GameObject sniperTowerPrefab;

    public int basicCost = 25;
    public int sniperCost = 40;

    GameObject currentPrefab;
    int currentCost;

    void Start()
    {
        SelectBasic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectBasic();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSniper();

        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p = new Vector2(mousePos.x, mousePos.y);

        Collider2D col = Physics2D.OverlapPoint(p);
        if (col == null) return;

        BuildSpot spot = col.GetComponent<BuildSpot>();
        if (spot == null) return;

        if (spot.occupied) return;

        if (MoneyManager.Instance != null && !MoneyManager.Instance.Spend(currentCost))
        {
            Debug.Log("돈 부족!");
            return;
        }

        Instantiate(currentPrefab, spot.transform.position, Quaternion.identity);
        spot.occupied = true;
    }

    void SelectBasic()
    {
        currentPrefab = basicTowerPrefab;
        currentCost = basicCost;
        Debug.Log("1: Basic Tower 선택");
    }

    void SelectSniper()
    {
        currentPrefab = sniperTowerPrefab;
        currentCost = sniperCost;
        Debug.Log("2: Sniper Tower 선택");
    }
}
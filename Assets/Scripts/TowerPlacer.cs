using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance { get; private set; }

    [Header("Tower Prefabs")]
    public GameObject basicTowerPrefab;
    public GameObject sniperTowerPrefab;
    public GameObject splashTowerPrefab;
    public GameObject slowTowerPrefab;

    [Header("Tower Costs")]
    public int basicCost = 25;
    public int sniperCost = 40;
    public int splashCost = 45;
    public int slowCost = 35;

    [Header("UI")]
    public TMP_Text selectedTowerText;

    [Header("Preview")]
    public Sprite previewRangeSprite;

    GameObject currentPrefab;
    int currentCost;
    string currentTowerName = "";
    bool isPlacing = false;

    GameObject placementPreview;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ClearSelection();
    }

    void Update()
    {
        UpdateSelectedText();

        if (!isPlacing) return;

        UpdatePreviewPosition();

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        // UI 클릭이면 설치 막기
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p = new Vector2(mousePos.x, mousePos.y);

        Collider2D col = Physics2D.OverlapPoint(p);
        if (col == null) return;

        BuildSpot spot = col.GetComponent<BuildSpot>();
        if (spot == null) return;

        if (spot.occupied)
        {
            Debug.Log("이미 설치된 자리다.");
            return;
        }

        if (currentPrefab == null)
        {
            Debug.LogWarning("현재 선택된 타워 프리팹이 없다.");
            return;
        }

        if (MoneyManager.Instance != null && !MoneyManager.Instance.Spend(currentCost))
        {
            Debug.Log("돈 부족!");
            return;
        }

        GameObject tower = Instantiate(currentPrefab, spot.transform.position, Quaternion.identity);

        spot.SetOccupied(tower);

        TowerSelectable selectable = tower.GetComponent<TowerSelectable>();
        if (selectable != null)
        {
            selectable.ownerSpot = spot;
        }

        Debug.Log(currentTowerName + " 설치 완료");

        // 이게 핵심: 설치 끝나면 배치 모드 종료
        CancelPlacement();
    }

    void UpdateSelectedText()
    {
        if (selectedTowerText == null) return;

        if (!isPlacing || currentPrefab == null)
        {
            selectedTowerText.text = "Selected: None";
            return;
        }

        selectedTowerText.text = "Selected: " + currentTowerName + " ($" + currentCost + ")";
    }

    void UpdatePreviewPosition()
    {
        if (placementPreview == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        placementPreview.transform.position = mousePos;
    }

    void CreatePreview()
    {
        DestroyPreview();

        if (currentPrefab == null) return;

        placementPreview = Instantiate(currentPrefab, Vector3.zero, Quaternion.identity);

        TowerShooter shooter = placementPreview.GetComponent<TowerShooter>();
        if (shooter != null)
            shooter.enabled = false;

        TowerSelectable selectable = placementPreview.GetComponent<TowerSelectable>();
        if (selectable != null)
            selectable.enabled = false;

        Collider2D col = placementPreview.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        SpriteRenderer towerSprite = placementPreview.GetComponent<SpriteRenderer>();
        if (towerSprite != null)
        {
            Color c = towerSprite.color;
            c.a = 0.6f;
            towerSprite.color = c;
        }

        GameObject rangeObj = new GameObject("PreviewRange");
        rangeObj.transform.SetParent(placementPreview.transform);
        rangeObj.transform.localPosition = Vector3.zero;
        rangeObj.layer = LayerMask.NameToLayer("Ignore Raycast");

        SpriteRenderer sr = rangeObj.AddComponent<SpriteRenderer>();
        sr.sprite = previewRangeSprite;
        sr.color = new Color(0f, 1f, 0f, 0.2f);
        sr.sortingOrder = 100;

        TowerShooter prefabShooter = currentPrefab.GetComponent<TowerShooter>();
        float range = 1f;
        if (prefabShooter != null)
            range = prefabShooter.range;

        float diameter = range * 2f;
        Vector3 parentScale = placementPreview.transform.lossyScale;

        float scaleX = diameter / parentScale.x;
        float scaleY = diameter / parentScale.y;

        rangeObj.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    void DestroyPreview()
    {
        if (placementPreview != null)
            Destroy(placementPreview);

        placementPreview = null;
    }

    public void SelectBasic()
    {
        currentPrefab = basicTowerPrefab;
        currentCost = basicCost;
        currentTowerName = "Basic Tower";
        isPlacing = true;
        CreatePreview();
    }

    public void SelectSniper()
    {
        currentPrefab = sniperTowerPrefab;
        currentCost = sniperCost;
        currentTowerName = "Sniper Tower";
        isPlacing = true;
        CreatePreview();
    }

    public void SelectSplash()
    {
        currentPrefab = splashTowerPrefab;
        currentCost = splashCost;
        currentTowerName = "Splash Tower";
        isPlacing = true;
        CreatePreview();
    }

    public void SelectSlow()
    {
        currentPrefab = slowTowerPrefab;
        currentCost = slowCost;
        currentTowerName = "Slow Tower";
        isPlacing = true;
        CreatePreview();
    }

    public void CancelPlacement()
    {
        ClearSelection();
    }

    void ClearSelection()
    {
        currentPrefab = null;
        currentCost = 0;
        currentTowerName = "";
        isPlacing = false;
        DestroyPreview();
    }
}
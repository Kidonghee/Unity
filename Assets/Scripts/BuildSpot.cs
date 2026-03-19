using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public bool occupied = false;
    public GameObject currentTower;

    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void SetOccupied(GameObject tower)
    {
        occupied = true;
        currentTower = tower;

        Debug.Log(name + " SetOccupied 호출");

        if (col != null)
        {
            col.enabled = false;
            Debug.Log(name + " BuildSpot Collider 꺼짐");
        }
        else
        {
            Debug.LogWarning(name + " Collider2D 없음");
        }
    }

    public void ClearSpot()
    {
        occupied = false;
        currentTower = null;

        Debug.Log(name + " ClearSpot 호출");

        if (col != null)
        {
            col.enabled = true;
            Debug.Log(name + " BuildSpot Collider 다시 켜짐");
        }
    }
}
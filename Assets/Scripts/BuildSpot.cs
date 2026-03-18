using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public bool occupied = false;
    public GameObject currentTower;

    public void ClearSpot()
    {
        occupied = false;
        currentTower = null;
    }
}
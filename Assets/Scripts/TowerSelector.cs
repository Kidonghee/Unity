using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static int SelectedTower = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SelectedTower = 1;
            Debug.Log("Tower 선택");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SelectedTower = 2;
            Debug.Log("SniperTower");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SelectedTower = 3;
            Debug.Log("SplashTower");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SelectedTower = 4;
            Debug.Log("SlowTower");
        }
    }
}
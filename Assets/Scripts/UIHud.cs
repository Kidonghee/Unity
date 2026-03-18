using TMPro;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text hpText;
    public TMP_Text waveText;
    public TMP_Text towerText;
    public TMP_Text centerMessageText;

    public BaseHealth baseHealth;

    void Update()
    {
        if (moneyText != null && MoneyManager.Instance != null)
            moneyText.text = "Money : " + MoneyManager.Instance.Money;

        if (hpText != null && baseHealth != null)
            hpText.text = "HP : " + baseHealth.CurrentHp + " / " + baseHealth.maxHp;

        if (waveText != null && WaveSpawner.Instance != null)
            waveText.text = "Wave : " + WaveSpawner.Instance.CurrentWave + " / " + WaveSpawner.Instance.TotalWaves;

        if (towerText != null)
            towerText.text = "Tower : " + TowerSelector.SelectedTower;

        if (centerMessageText == null || baseHealth == null)
            return;

        if (baseHealth.IsGameOver)
        {
            centerMessageText.text = "GAME OVER";
        }
        else if (WaveSpawner.Instance != null && WaveSpawner.Instance.IsClear)
        {
            centerMessageText.text = "CLEAR";
        }
        else
        {
            centerMessageText.text = "";
        }
    }
}
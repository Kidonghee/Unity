using TMPro;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text hpText;
    public TMP_Text waveText;

    void Update()
    {
        if (moneyText != null)
        {
            if (MoneyManager.Instance != null)
                moneyText.text = $"Money: {MoneyManager.Instance.Money}";
            else
                moneyText.text = "Money: (no manager)";
        }

        if (hpText != null)
        {
            if (BaseHealth.Instance != null)
                hpText.text = $"HP: {BaseHealth.Instance.CurrentHp}/{BaseHealth.Instance.maxHp}";
            else
                hpText.text = "HP: (no base)";
        }

        if (waveText != null)
        {
            if (WaveSpawner.Instance != null)
                waveText.text = $"Wave: {WaveSpawner.Instance.CurrentWave}/{WaveSpawner.Instance.TotalWaves}";
            else
                waveText.text = "Wave: (no spawner)";
        }
    }
}
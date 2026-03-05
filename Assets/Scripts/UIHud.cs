using TMPro;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text hpText;
    public TMP_Text waveText;

    void Update()
    {
        if (MoneyManager.Instance != null)
            moneyText.text = "Money: " + MoneyManager.Instance.Money;

        if (BaseHealth.Instance != null)
            hpText.text = "HP: " + BaseHealth.Instance.CurrentHp + "/" + BaseHealth.Instance.maxHp;

        if (WaveSpawner.Instance != null)
            waveText.text = "Wave: " + WaveSpawner.Instance.CurrentWave + "/" + WaveSpawner.Instance.TotalWaves;
    }
}
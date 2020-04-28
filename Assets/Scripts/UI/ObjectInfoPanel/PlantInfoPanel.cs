using TMPro;
using UnityEngine;

public class PlantInfoPanel : ObjectInfoPanel
{
    [SerializeField]
    private PlantConfiguration config;

    [SerializeField]
    private TMP_Text maxHealthLabel;
    [SerializeField]
    private TMP_Text energyLabel;

    private void Awake()
    {
        healthLabel.text = config.startHealth.ToString();
        energyLabel.text = config.incomePerSecond.ToString();
    }
}

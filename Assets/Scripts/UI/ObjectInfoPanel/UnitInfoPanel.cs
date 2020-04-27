using TMPro;
using UnityEngine;

public class UnitInfoPanel : ObjectInfoPanel
{
    [SerializeField]
    private UnitConfiguration config;

    [SerializeField]
    private TMP_Text maxHealthLabel;
    [SerializeField]
    private TMP_Text damageLabel;
    [SerializeField]
    private TMP_Text attackSpeedLabel;
    [SerializeField]
    private TMP_Text speedLabel;

    private void Awake()
    {
        healthText.text = config.startHealth.ToString();
        maxHealthLabel.text = config.startHealth.ToString();
        damageLabel.text = config.damage.ToString();
        attackSpeedLabel.text = (1 / config.attackingInterval).ToString("#.##");
        speedLabel.text = config.speed.ToString();
    }
}

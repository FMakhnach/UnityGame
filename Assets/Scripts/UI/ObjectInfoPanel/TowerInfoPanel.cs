using TMPro;
using UnityEngine;

public class TowerInfoPanel : ObjectInfoPanel
{
    [SerializeField]
    private TowerConfiguration config;

    [SerializeField]
    private TMP_Text maxHealthLabel;
    [SerializeField]
    private TMP_Text damageLabel;
    [SerializeField]
    private TMP_Text attackSpeedLabel;

    private void Awake()
    {
        healthText.text = config.startHealth.ToString();
        maxHealthLabel.text = config.startHealth.ToString();
        damageLabel.text = config.damage.ToString();
        attackSpeedLabel.text = (1 / config.attackingInterval).ToString("#.##");
    }
}

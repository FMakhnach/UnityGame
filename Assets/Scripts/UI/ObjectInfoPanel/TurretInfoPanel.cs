using TMPro;
using UnityEngine;

/// <summary>
/// Info panel that represents turret stats.
/// </summary>
public class TurretInfoPanel : ObjectInfoPanel
{
    [SerializeField]
    private TurretConfiguration config;
    [SerializeField]
    private TMP_Text maxHealthLabel;
    [SerializeField]
    private TMP_Text damageLabel;
    [SerializeField]
    private TMP_Text attackSpeedLabel;

    private void Awake()
    {
        healthLabel.text = config.startHealth.ToString();
        maxHealthLabel.text = config.startHealth.ToString();
        damageLabel.text = config.damage.ToString();
        attackSpeedLabel.text = (1 / config.attackingInterval).ToString("#.##");
    }
}

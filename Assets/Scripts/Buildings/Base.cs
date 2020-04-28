using UnityEngine;

public class Base : MonoBehaviour, ITarget, IDamageable
{
    /// <summary>
    /// Base owner.
    /// </summary>
    [SerializeField]
    private PlayerManager owner;
    [SerializeField]
    private float incomePerSecond;
    private DamageableBehaviour damageableBehaviour;
    private BaseInfoPanel Panel;

    public Transform TargetPoint => transform;
    public PlayerManager Owner => owner;

    /// <summary>
    /// Recieves damage. If destroyed, the owner loses.
    /// </summary>
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(incomePerSecond);
            GameManager.Instance.EndGame(from, owner);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        owner.IncreaseIncome(incomePerSecond);

        Panel = ObjectInfoPanelController.Instance.Base;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
        string health = ((int)damageableBehaviour.Health).ToString();
        Panel.maxHealth.text = health;
        Panel.healthLabel.text = health;
        Panel.energyIncome.text = incomePerSecond.ToString("#.##");
    }
}
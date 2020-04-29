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
    [SerializeField]
    private BaseInfoPanel panel;

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

        GetComponentInChildren<OnMouseOverInfoPanel>().panel = panel;
        damageableBehaviour.healthText = panel.healthLabel;
        string health = ((int)damageableBehaviour.Health).ToString();
        panel.maxHealth.text = health;        
        panel.healthLabel.text = health;
        panel.regeneration.text = damageableBehaviour.Regeneration.ToString("0.##");
        panel.energyIncome.text = incomePerSecond.ToString("0.##");
    }
}
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

    public Transform TargetPoint => transform;
    public PlayerManager Owner => owner;

    /// <summary>
    /// Recieves damage. If destroyed, the owner loses.
    /// </summary>
    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(incomePerSecond);
            GameManager.Instance.LoseGame(owner);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        owner.IncreaseIncome(incomePerSecond);
    }
}
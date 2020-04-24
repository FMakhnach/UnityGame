using TMPro;
using UnityEngine;

public class Base : MonoBehaviour, ITarget, IDamageable
{
    /// <summary>
    /// Base owner.
    /// </summary>
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private float incomePerSecond;
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => transform;
    public Alignment Alignment => playerManager.Alignment;

    /// <summary>
    /// Recieves damage.
    /// </summary>
    public void ReceiveDamage(float damage)
        => damageableBehaviour.ReceiveDamage(damage);

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        playerManager.IncreaseIncome(incomePerSecond);
    }
    private void OnDestroy()
    {
        playerManager.DecreaseIncome(incomePerSecond);
    }
}
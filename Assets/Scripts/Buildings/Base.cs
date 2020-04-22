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
    /// Recieves damage, if loses all heath - triggers game over.
    /// </summary>
    public void ReceiveDamage(float damage)
        => damageableBehaviour.ReceiveDamage(damage);

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        playerManager.AddIncome(incomePerSecond);
    }
    private void OnDestroy()
    {
        playerManager.DecreaseIncome(incomePerSecond);
    }
}
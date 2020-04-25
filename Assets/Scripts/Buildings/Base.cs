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
    /// Recieves damage. If destroyed, the owner loses.
    /// </summary>
    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            playerManager.DecreaseIncome(incomePerSecond);
            GameManager.Instance.LoseGame(playerManager);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        playerManager.IncreaseIncome(incomePerSecond);
    }
}
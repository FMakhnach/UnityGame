using UnityEngine;

public class Base : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    private float startHealth;
    private float currentHealth;
    [SerializeField]
    private PlayerManager playerManager;

    public float Health => currentHealth;
    public Transform TargetPoint => transform;

    public Alignment Alignment => playerManager.Alignment;

    public void RecieveDamage(float damage)
    {
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            Time.timeScale = 0;
            playerManager.LoseGame();
        }
        else
        {
            currentHealth -= damage;
        }
    }
    private void Awake()
    {
        currentHealth = startHealth;
    }
}

using UnityEngine;

public class Base : MonoBehaviour, ITarget, IDamageable
{
    /// <summary>
    /// Amount of health the base has at the start of the level.
    /// </summary>
    [SerializeField]
    private float startHealth;
    /// <summary>
    /// Base owner.
    /// </summary>
    [SerializeField]
    private PlayerManager playerManager;
    /// <summary>
    /// Particles that play on destroy.
    /// </summary>
    [SerializeField]
    private ParticleSystem destroyParticles;
    /// <summary>
    /// Current health of the base.
    /// </summary>
    private float currentHealth;

    /// <summary>
    /// Current health of the base.
    /// </summary>
    public float Health => currentHealth;
    public Transform TargetPoint => transform;
    public Alignment Alignment => playerManager.Alignment;

    /// <summary>
    /// Recieves damage, if loses all heath - triggers game over.
    /// </summary>
    public void ReceiveDamage(float damage)
    {
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            var ps = Instantiate(destroyParticles, transform.position, transform.rotation);
            ps.Play();
            Destroy(ps.gameObject, 2f);
            playerManager.LoseGame();
            Destroy(this.gameObject);
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
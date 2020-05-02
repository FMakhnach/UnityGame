using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DamageableBehaviour))]
public abstract class Turret : MonoBehaviour, ITarget, IDamageable
{
    /// <summary>
    /// Turret stats are there.
    /// </summary>
    [SerializeField]
    protected TurretConfiguration config;
    /// <summary>
    /// Point for targeting.
    /// </summary>
    [SerializeField]
    private Transform ownTarget;
    /// <summary>
    /// Protected cause I use it to play fire sounds in AttackingTower.
    /// </summary>
    protected AudioSource audioSource;
    private PlayerManager owner;
    /// <summary>
    /// Thing is responsible for damage and health.
    /// Protected cause I connect it to a valid UI panel in children
    /// to show health.
    /// </summary>
    protected DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => ownTarget;
    public PlayerManager Owner
    {
        get => owner;
        set
        {
            if (owner == null && value != null)
            {
                owner = value;
            }
        }
    }
    public TurretInfoPanel Panel { get; protected set; }

    public void PlaceOn(TurretPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f * audioSource.volume);
    }
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            from.TurretKilled();
            owner.TurretLost();
            Destroy(gameObject);
        }
    }

    protected virtual void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        audioSource = GetComponent<AudioSource>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}

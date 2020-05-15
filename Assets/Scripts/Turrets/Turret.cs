using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DamageableBehaviour))]
public abstract class Turret : MonoBehaviour, ITarget, IDamageable, IPoolable
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
    /// <summary>
    /// Thing is responsible for damage and health.
    /// Protected cause I connect it to a valid UI panel in children
    /// to show health.
    /// </summary>
    protected DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => ownTarget;
    public PlayerManager Owner { get; set; }
    public TurretInfoPanel Panel { get; protected set; }

    public void PlaceOn(TurretPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
    }
    public void PlaySpawnSound()
        => audioSource.PlayOneShot(config.spawnSound, 0.3f * audioSource.volume);
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            from.TurretKilled();
            Owner.TurretLost();

            var ps = PoolManager.Instance.GetTurretExplosion();
            ps.transform.position = transform.position;
            ps.transform.rotation = transform.rotation;
            ps.gameObject.SetActive(true);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(config.destroySound, 0.3f);
            PoolManager.Instance.Reclaim(ps, config.destroySound.length + 0.5f);

            PoolManager.Instance.Reclaim(this);
        }
    }

    protected virtual void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        audioSource = GetComponent<AudioSource>();
    }
    public abstract void ResetValues();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}

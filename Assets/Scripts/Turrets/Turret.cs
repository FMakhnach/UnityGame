using UnityEngine;

public abstract class Turret : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    protected TurretConfiguration config;
    [SerializeField]
    private Transform ownTarget;
    protected LayerMask targetableMask;
    protected AudioSource audioSource;
    private PlayerManager owner;
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

    protected virtual void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
    }
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
            Destroy(this.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}

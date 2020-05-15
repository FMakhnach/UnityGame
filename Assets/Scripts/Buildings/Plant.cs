using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DamageableBehaviour))]
public class Plant : MonoBehaviour, ITarget, IDamageable, IPoolable
{
    [SerializeField]
    private PlantConfiguration config;
    [SerializeField]
    private Transform targetPoint;
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => targetPoint;
    public PlayerManager Owner { get; set; }
    public PlantInfoPanel Panel { get; private set; }

    public void PlaceOn(PlantPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        Owner.IncreaseIncome(config.incomePerSecond);
    }
    public void PlaySpawnSound()
        => GetComponent<AudioSource>().PlayOneShot(config.spawnSound, 0.3f);
    /// <summary>
    /// Receives damage. If loses all HP, destroys and the owner loses additional income.
    /// </summary>
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            Owner.DecreaseIncome(config.incomePerSecond);

            var ps = PoolManager.Instance.GetBigExplosion();
            ps.transform.position = transform.position;
            ps.transform.rotation = transform.rotation;
            ps.gameObject.SetActive(true);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(config.destroySound, 0.3f);
            PoolManager.Instance.Reclaim(ps, config.destroySound.length + 0.5f);

            PoolManager.Instance.Reclaim(this);
        }
    }
    public void ResetValues()
    {
        Owner = default;
        damageableBehaviour.ResetValues();
    }

    private void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
    }
    private void Start()
    {
        // Initializing GUI panel with correct data.
        Panel = ObjectInfoPanelController.Instance.Plant;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}

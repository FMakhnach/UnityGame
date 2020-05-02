using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DamageableBehaviour))]
public class Plant : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    private PlantConfiguration config;
    [SerializeField]
    private Transform targetPoint;
    private PlayerManager owner;
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => targetPoint;
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
    public PlantInfoPanel Panel { get; private set; }


    public void PlaceOn(PlantPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        owner.IncreaseIncome(config.incomePerSecond);
        GetComponent<AudioSource>().PlayOneShot(config.spawnSound, 0.3f);
    }
    /// <summary>
    /// Receives damage. If loses all HP, destroys and the owner loses additional income.
    /// </summary>
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(config.incomePerSecond);
            Destroy(this.gameObject);
        }
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

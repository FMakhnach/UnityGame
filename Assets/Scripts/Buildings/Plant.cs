using UnityEngine;

public class Plant : MonoBehaviour, ITarget, IDamageable
{
    public const int Cost = 60;

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

    private void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
    }
    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Plant;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }

    public void PlaceOn(PlantPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        owner.IncreaseIncome(config.incomePerSecond);
        GetComponent<AudioSource>().PlayOneShot(config.spawnSound, 0.3f);
    }

    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(config.incomePerSecond);
            Destroy(this.gameObject);
        }
    }
}

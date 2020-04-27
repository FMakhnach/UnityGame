using UnityEngine;

public class Plant : MonoBehaviour, ITarget, IDamageable
{
    [CreateAssetMenu(menuName = "Plant Factory")]
    public class Factory : ScriptableObject
    {
        [SerializeField]
        private Plant plantPrefab;

        public Plant CreatePlant(PlayerManager owner)
        {
            Plant plant = Instantiate(plantPrefab);
            plant.owner = owner;
            return plant;
        }
    }

    public const int Cost = 25;

    [SerializeField]
    private PlantConfiguration config;
    [SerializeField]
    private Transform targetPoint;
    private PlayerManager owner;
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => targetPoint;

    public PlayerManager Owner => owner;
    public PlantInfoPanel Panel { get; private set; }

    private void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
    }
    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Plant;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthText;
    }

    public void PlaceOn(PlantPlacement place, PlayerManager owner)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        this.owner = owner;
        owner.IncreaseIncome(config.incomePerSecond);
        GetComponent<AudioSource>().PlayOneShot(config.spawnSound, 0.3f);
    }

    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(config.incomePerSecond);
            Destroy(this.gameObject);
        }
    }
}

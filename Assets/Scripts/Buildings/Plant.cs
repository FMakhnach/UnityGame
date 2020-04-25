using UnityEngine;

public class Plant : MonoBehaviour, ITarget, IDamageable
{
    public const int Cost = 25;

    [SerializeField]
    private float incomePerSecond;
    [SerializeField]
    private Transform targetPoint;
    [SerializeField]
    private AudioClip placingSound;
    private PlayerManager owner;
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => targetPoint;

    public Alignment Alignment => owner.Alignment;

    private void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
    }

    public void PlaceOn(PlantPlacement place, PlayerManager owner)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        this.owner = owner;
        owner.IncreaseIncome(incomePerSecond);
        GetComponent<AudioSource>().PlayOneShot(placingSound, 0.3f);
    }

    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            owner.DecreaseIncome(incomePerSecond);
            Destroy(this.gameObject);
        }
    }
}

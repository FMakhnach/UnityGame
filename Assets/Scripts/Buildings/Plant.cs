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

    public void PlaceOn(Vector3 placePoint, Quaternion rotation, PlayerManager owner)
    {
        transform.position = placePoint;
        transform.rotation = rotation;
        this.owner = owner;
        owner.IncreaseIncome(incomePerSecond);
        GetComponent<AudioSource>().PlayOneShot(placingSound, 0.3f);
    }

    public void ReceiveDamage(float damage)
        => damageableBehaviour.ReceiveDamage(damage);

    private void OnDestroy()
    {
        owner.DecreaseIncome(incomePerSecond);
    }
}

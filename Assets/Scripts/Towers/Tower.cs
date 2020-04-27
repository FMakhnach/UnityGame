using UnityEngine;

public class Tower : MonoBehaviour, ITarget, IDamageable
{
    [CreateAssetMenu(menuName = "Tower Factory")]
    public class Factory : ScriptableObject
    {
        [SerializeField]
        private LaserTower laserTowerPrefab;
        [SerializeField]
        private MachineGunTower mgTowerPrefab;

        [SerializeField]
        private Material laserTowerMaterial;
        [SerializeField]
        private Material mgTowerMaterial;

        private Vector3 spawnPosition = new Vector3(0f, 100f, 0f);

        public LaserTower CreateLaserTower(PlayerManager owner)
        {
            LaserTower tower = Instantiate(laserTowerPrefab, spawnPosition, Quaternion.identity);
            tower.owner = owner;
            foreach (var renderer in tower.gameObject.GetComponentsInChildren<Renderer>())
            {
                if (renderer.gameObject.CompareTag("MaterialChange"))
                {
                    renderer.material = laserTowerMaterial;
                }

            }
            return tower;
        }
        public MachineGunTower CreateMGTower(PlayerManager owner)
        {
            MachineGunTower tower = Instantiate(mgTowerPrefab, spawnPosition, Quaternion.identity);
            tower.owner = owner;
            foreach (var renderer in tower.gameObject.GetComponentsInChildren<Renderer>())
            {
                if (renderer.gameObject.CompareTag("MaterialChange"))
                {
                    renderer.material = mgTowerMaterial;
                }
            }
            return tower;
        }
    }

    [SerializeField]
    protected TowerConfiguration config;
    [SerializeField]
    private Transform ownTarget;
    protected LayerMask targetableMask;
    protected AudioSource audioSource;
    private PlayerManager owner;
    protected DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => ownTarget;
    public PlayerManager Owner => owner;
    public TowerInfoPanel Panel { get; protected set; }

    protected virtual void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
    }
    public void PlaceOn(TowerPlacement place)
    {
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f * audioSource.volume);
    }

    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
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

using UnityEngine;

[RequireComponent(typeof(DamageableBehaviour))]
[RequireComponent(typeof(OnMouseOverInfoPanel))]
public class Base : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    private PlayerManager owner;
    [SerializeField]
    private BaseConfiguration config;
    [SerializeField]
    private Transform targetPoint;
    /// <summary>
    /// Damage logic keeper.
    /// </summary>
    private DamageableBehaviour damageableBehaviour;
    /// <summary>
    /// GUI panel for showing information about the base.
    /// </summary>
    [SerializeField]
    private BaseInfoPanel panel;

    public Transform TargetPoint => targetPoint;
    public PlayerManager Owner => owner;

    /// <summary>
    /// Recieves damage. If destroyed, the owner loses.
    /// </summary>
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            // Getting particles and playing them.
            var ps = PoolManager.Instance.GetBigExplosion();
            ps.transform.position = transform.position;
            ps.transform.rotation = transform.rotation;
            ps.gameObject.SetActive(true);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(config.destroySound, 0.3f);
            PoolManager.Instance.Reclaim(ps.gameObject, config.destroySound.length + 0.2f);

            gameObject.SetActive(false);
            LevelManager.Instance.EndGame(from, owner);
        }
    }

    private void Start()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        damageableBehaviour.healthText = panel.healthLabel;

        // Initializing info panel with valid data.
        GetComponent<OnMouseOverInfoPanel>().panel = panel;
        string health = ((int)damageableBehaviour.Health).ToString();
        panel.maxHealth.text = health;
        panel.healthLabel.text = health;
        panel.regeneration.text = damageableBehaviour.Regeneration.ToString("0.##");
    }
}
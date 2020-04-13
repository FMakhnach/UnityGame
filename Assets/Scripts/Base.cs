using UnityEngine;

public class Base : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    private float startHealth;
    private float currentHealth;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private AudioClip destroySound;
    [SerializeField]
    private ParticleSystem destroyParticles;

    public float Health => currentHealth;
    public Transform TargetPoint => transform;

    public Alignment Alignment => playerManager.Alignment;

    public void RecieveDamage(float damage)
    {
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            var ps = Instantiate(destroyParticles, transform.position, transform.rotation);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(destroySound, 0.5f);
            Destroy(ps.gameObject, destroySound.length + 0.2f);
            playerManager.LoseGame();
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth -= damage;
        }
    }
    private void Awake()
    {
        currentHealth = startHealth;
    }
}

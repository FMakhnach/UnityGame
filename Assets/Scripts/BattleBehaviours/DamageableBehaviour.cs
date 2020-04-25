using TMPro;
using UnityEngine;

public class DamageableBehaviour : MonoBehaviour
{
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private ParticleSystem destroyParticles;
    [SerializeField]
    private AudioClip destroySound;
    [SerializeField]
    private TMP_Text healthBar;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = startHealth;
        healthBar.text = ((int)currentHealth).ToString();
    }

    /// <summary>
    /// Recieves damage, returns true if the object lost all its health. Updates health bar also.
    /// </summary>
    public bool ReceiveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            var ps = Instantiate(destroyParticles, transform.position, transform.rotation);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(destroySound, 0.3f);
            Destroy(ps.gameObject, destroySound.length + 0.5f);
            return true;
        }
        else
        {
            currentHealth -= damage;
            int health = (int)(currentHealth);
            healthBar.text = (health == 0 ? 1 : health).ToString();
            return false;
        }
    }
}

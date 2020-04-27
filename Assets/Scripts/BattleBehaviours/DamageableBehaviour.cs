using TMPro;
using UnityEngine;

public class DamageableBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObjectConfig config;
    [HideInInspector]
    public TMP_Text healthText;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = config.startHealth;
    }

    /// <summary>
    /// Recieves damage, returns true if the object lost all its health. Updates health bar also.
    /// </summary>
    public bool ReceiveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            var ps = Instantiate(config.destroyParticles, transform.position, transform.rotation);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(config.destroySound, 0.3f);
            Destroy(ps.gameObject, config.destroySound.length + 0.5f);
            return true;
        }
        else
        {
            currentHealth -= damage;
            int health = (int)(currentHealth);
            healthText.text = (health == 0 ? 1 : health).ToString();
            return false;
        }
    }
}

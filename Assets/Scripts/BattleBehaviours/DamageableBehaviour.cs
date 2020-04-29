using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// Represents object that can be damaged and destroyed.
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    /// <summary>
    /// Need it just for start health.
    /// </summary>
    [SerializeField]
    private GameObjectConfig config;
    /// <summary>
    /// UI label that shows current HP to the player.
    /// </summary>
    [HideInInspector]
    public TMP_Text healthText;

    private float currentHealth;
    public float Health => currentHealth;
    public float Regeneration => config.regeneration;

    private void Awake()
    {
        currentHealth = config.startHealth;
    }
    private void Update()
    {
        if(currentHealth < config.startHealth)
        {
            currentHealth += Regeneration * Time.deltaTime;
            healthText.text = ((int)currentHealth).ToString();
        }
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
            if (healthText != null)
            {
                healthText.text = (health == 0 ? 1 : health).ToString();
            }
            return false;
        }
    }
}

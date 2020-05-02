using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents object that can be damaged and healed.
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    /// <summary>
    /// Need it just for start health.
    /// </summary>
    [SerializeField]
    private GameObjectConfig config;
    private float currentHealth;

    /// <summary>
    /// UI label that shows current HP to the player.
    /// </summary>
    [HideInInspector]
    public TMP_Text healthText;

    public float Health => currentHealth;
    /// <summary>
    /// Amount of health points that is regenerated in a second.
    /// </summary>
    public float Regeneration => config.regeneration;

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
            UpdateHealthLabel();
            return false;
        }
    }
    /// <summary>
    /// Heals the object (can't be more than max hp obviously).
    /// </summary>
    /// <param name="heal"></param>
    public void ReceiveHeal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > config.startHealth)
        {
            currentHealth = config.startHealth;
        }
        UpdateHealthLabel();
    }

    private void Awake()
    {
        currentHealth = config.startHealth;
        if (Regeneration > 0f)
        {
            StartCoroutine("Regenerate");
        }
    }
    /// <summary>
    /// Sets actual value to health text.
    /// </summary>
    private void UpdateHealthLabel()
    {
        if (healthText != null)
        {
            int health = (int)(currentHealth);
            healthText.text = (health == 0 ? 1 : health).ToString();
        }
    }
    /// <summary>
    /// Regenerates health every second if it can be done.
    /// </summary>
    private IEnumerator Regenerate()
    {
        for (; ; )
        {
            if (currentHealth < config.startHealth)
            {
                currentHealth += Regeneration;
                UpdateHealthLabel();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}

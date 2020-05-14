using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents object that can be damaged.
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    /// <summary>
    /// Need it for start health and regeneration.
    /// </summary>
    [SerializeField]
    private GameObjectConfig config;
    private float health;

    /// <summary>
    /// UI label that shows current HP to the player.
    /// </summary>
    [HideInInspector]
    public TMP_Text healthText;

    public float Health => health;
    /// <summary>
    /// Amount of health points that is regenerated in a second.
    /// </summary>
    public float Regeneration => config.regeneration;

    /// <summary>
    /// Recieves damage, returns true if the object lost all its health. Updates health bar also.
    /// </summary>
    public bool ReceiveDamage(float damage)
    {
        if (gameObject != null && damage >= health)
        {
            ObjectInfoPanelController.Instance.UnlockPanel();
            return true;
        }
        else
        {
            health -= damage;
            UpdateHealthLabel();
            return false;
        }
    }
    /// <summary>
    /// Is used for Pool manager.
    /// </summary>
    public void ResetValues()
    {
        health = config.startHealth;
    }

    private void Awake()
    {
        ResetValues();
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
        if (healthText != null && ObjectInfoPanelController.Instance.Target == this)
        {
            int health = (int)(this.health);
            if (health == 0)
            {

            }
            else
            {
                healthText.text = health.ToString();
            }
        }
    }
    /// <summary>
    /// Regenerates health every second if not max health.
    /// </summary>
    private IEnumerator Regenerate()
    {
        for (; ; )
        {
            health += Regeneration;
            if (health > config.startHealth)
            {
                health = config.startHealth;
            }
            UpdateHealthLabel();
            yield return new WaitForSeconds(1f);
        }
    }
}

using UnityEngine;

/// <summary>
/// Some stats that are shared among turrets, units, plants.
/// </summary>
public class GameObjectConfig : ScriptableObject
{
    public float startHealth;
    public float regeneration;
    public ParticleSystem destroyParticles;
    public AudioClip destroySound;
    public AudioClip spawnSound;
}

using UnityEngine;

/// <summary>
/// Some stats that are shared among the gameobjects like turrets, units, etc.
/// </summary>
public class GameObjectConfig : ScriptableObject
{
    public AudioClip spawnSound;
    public float startHealth;
    public float regeneration;
    public ParticleSystem destroyParticles;
    public AudioClip destroySound;
}

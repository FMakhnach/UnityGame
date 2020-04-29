using UnityEngine;

public class GameObjectConfig : ScriptableObject
{
    public AudioClip spawnSound;
    public float startHealth;
    public float regeneration;
    public ParticleSystem destroyParticles;
    public AudioClip destroySound;
}

using UnityEngine;

[CreateAssetMenu]
public class TowerConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip destroySound;
    public float radius;
    public float attackingInterval;
    public float health;
    public ParticleSystem destroyParticles;
}

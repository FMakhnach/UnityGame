using UnityEngine;

[CreateAssetMenu]
public class UnitConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip destroySound;
    public float radius;
    public float attackingInterval;
    public float health;
    public float speed;
    public ParticleSystem destroyParticles;
}

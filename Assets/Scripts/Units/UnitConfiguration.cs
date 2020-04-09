using UnityEngine;

[CreateAssetMenu]
public class UnitConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public float radius;
    public float damage;
    public float attackingInterval;
    public float health;
    public float speed;
}

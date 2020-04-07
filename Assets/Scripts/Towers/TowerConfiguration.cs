using UnityEngine;

[CreateAssetMenu]
public class TowerConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public float radius;
    public float damage;
    public float attackingInterval;
    public float projectileSpeed;
    /*public float health;
    public int cost;*/
}

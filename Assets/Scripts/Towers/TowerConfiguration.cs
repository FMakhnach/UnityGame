using UnityEngine;

[CreateAssetMenu]
public class TowerConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public float radius;
    public float damage;
    public float attackingInterval;
    public float health;
    //public int cost;
}

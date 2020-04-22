using UnityEngine;

[CreateAssetMenu]
public class TowerConfiguration : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public float radius;
    public float attackingInterval;
}

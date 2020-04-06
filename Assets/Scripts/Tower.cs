using UnityEngine;

public class Tower : MonoBehaviour, IPlaceable<TowerTile>
{
    [SerializeField]
    private TowerConfiguration config;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaceOn(TowerTile tile)
    {
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
        transform.position = tile.transform.position;
        tile.RecieveTower(this);
    }
}

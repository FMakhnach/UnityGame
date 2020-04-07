using UnityEngine;

public class Unit : MonoBehaviour, ISpawnable<SpawnTile>, IDamageable, ITarget
{
    /// <summary>
    /// Unit's stats and stuff.
    /// </summary>
    [SerializeField]
    private UnitConfiguration config;
    /// <summary>
    /// Current destination. Null indicates absence of destination.
    /// </summary>
    private Vector3? destination;
    /// <summary>
    /// Path of the unit. 
    /// </summary>
    private Tile[] path;
    private int currentDestinationTileId = 0;
    /// <summary>
    /// Audio source for sound effects.
    /// </summary>
    private AudioSource audioSource;

    private float currentHealth;
    private float currentSpeed;

    private Alignment alignment;
    public Alignment Alignment => alignment;

    public Transform TargetPoint => transform;

    /// <summary>
    /// Is called when stepping into a new tile collider.
    /// </summary>
    public void UpdateDestination(Tile reachedTile)
    {
        // If we really stepped onto the wanted tile.
        if (reachedTile == path[currentDestinationTileId])
        {
            Tile nextTile = GetNextTileOnPath();
            if (nextTile != null)
            {
                destination = nextTile.transform.position;
            }
        }
    }
    public void SpawnOn(SpawnTile spawn, Alignment alignment)
    {
        this.alignment = alignment;
        path = spawn.Path;
        destination = path[0].transform.position;
        transform.position = spawn.transform.position;
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = config.health;
        currentSpeed = config.speed;
    }
    private void Update()
    {
        if (destination != null)
        {
            MoveToDestination();
        }
    }
    /// <summary>
    /// Moves the unit towards the destination point.
    /// </summary>
    private void MoveToDestination()
    {
        Vector3 deltaPath = (Vector3)destination - transform.position;
        deltaPath.y = 0;
        deltaPath = deltaPath.normalized * currentSpeed * Time.deltaTime;
        transform.Translate(deltaPath);
        // This looks pretty shitty.
        if ((transform.position - (Vector3)destination).sqrMagnitude < 0.001f)
        {
            destination = null;
            Debug.Log("Quest Completed!");
        }
    }
    private Tile GetNextTileOnPath()
        => currentDestinationTileId == (path.Length - 1) ? null : path[++currentDestinationTileId];

    public void RecieveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        currentHealth -= damage;
    }
}

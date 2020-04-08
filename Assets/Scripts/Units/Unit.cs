using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISpawnable<SpawnTile>, IDamageable, ITarget
{
    /// <summary>
    /// Unit's stats and stuff.
    /// </summary>
    [SerializeField]
    private UnitConfiguration config;
    /// <summary>
    /// Unit's own target point.
    /// </summary>
    [SerializeField]
    private Transform ownTarget;
    /// <summary>
    /// Current destination.
    /// </summary>
    private Tile destination;
    /// <summary>
    /// Path that the unit is following
    /// </summary>
    private Tile[] path;
    /// <summary>
    /// Current destination point index in array.
    /// </summary>
    private int curDestId;
    /// <summary>
    /// Audio source for sound effects.
    /// </summary>
    private AudioSource audioSource;
    private float currentHealth;
    private float currentSpeed;
    private bool pathIsNotComplete;
    private Alignment alignment;

    public Alignment Alignment => alignment;
    public Transform TargetPoint => ownTarget;

    public void SpawnOn(SpawnTile spawn, Alignment alignment)
    {
        this.alignment = alignment;
        path = spawn.Path;
        destination = path[0];
        pathIsNotComplete = true;
        transform.position = spawn.transform.position;
        transform.LookAt(destination.transform);
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
        curDestId = 0;
    }
    public void RecieveDamage(float damage)
    {
        Debug.LogError(currentHealth);
        if (gameObject != null && damage >= currentHealth)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        currentHealth -= damage;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = config.health;
        currentSpeed = config.speed;
    }
    private void Update()
    {
        if (pathIsNotComplete)
        {
            MoveToDestination();
        }
    }
    /// <summary>
    /// Moves the unit towards the destination point.
    /// </summary>
    private void MoveToDestination()
    {
        Vector3 deltaPath = destination.transform.position - transform.position;
        deltaPath.y = 0;
        transform.position += deltaPath.normalized * currentSpeed * Time.deltaTime;

        if ((transform.position - destination.transform.position).sqrMagnitude < 0.001f)
        {
            if (destination == path[path.Length - 1])
            {
                pathIsNotComplete = false;
                Debug.Log("Quest Completed!");
            }
            else
            {
                destination = path[++curDestId];
                transform.LookAt(destination.transform);
            }
        }
    }
}

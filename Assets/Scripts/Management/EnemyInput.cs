using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dummy class for spawning enemies (debug and test mostly).
/// </summary>
public class EnemyInput : MonoBehaviour
{
    private PlayerManager enemyManager;
    [SerializeField]
    private Toggle northRoad;
    [SerializeField]
    private Toggle middleRoad;
    [SerializeField]
    private Toggle southRoad;
    [SerializeField]
    private Spawn northSpawn;
    [SerializeField]
    private Spawn middleSpawn;
    [SerializeField]
    private Spawn southSpawn;

    public void SpawnBuggy()
    {
        if (northRoad.isOn)
        {
            enemyManager.SpawnBuggy(northSpawn);
        }
        else if (middleRoad.isOn)
        {
            enemyManager.SpawnBuggy(middleSpawn);
        }
        else if (southRoad.isOn)
        {
            enemyManager.SpawnBuggy(southSpawn);
        }
    }
    public void SpawnCopter()
    {
        if (northRoad.isOn)
        {
            enemyManager.SpawnCopter(northSpawn);
        }
        else if (middleRoad.isOn)
        {
            enemyManager.SpawnCopter(middleSpawn);
        }
        else if (southRoad.isOn)
        {
            enemyManager.SpawnCopter(southSpawn);
        }
    }
    private void Awake()
    {
        enemyManager = GetComponent<PlayerManager>();
    }
}

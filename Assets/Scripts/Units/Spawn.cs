using System.Linq;
using UnityEngine;

/// <summary>
/// Represents spawn. Gives road information for unit spawned.
/// </summary>
public class Spawn : PlaceArea
{
    /// <summary>
    /// Road points container.
    /// </summary>
    [SerializeField]
    private Transform[] roadConfig;
    /// <summary>
    /// Actual road.
    /// </summary>
    private Vector3[] road;

    public Vector3[] Road => road;

    private void Awake()
    {
        road = roadConfig.Select(x => x.position).ToArray();
    }
}
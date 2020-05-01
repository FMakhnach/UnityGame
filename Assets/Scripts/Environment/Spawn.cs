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

    public Vector3[] GetRoad() => roadConfig.Select(x => x.position).ToArray();
}
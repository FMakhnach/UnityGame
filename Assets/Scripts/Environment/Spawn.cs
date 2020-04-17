using UnityEngine;

/// <summary>
/// Represents spawn. Gives road information for unit spawned.
/// </summary>
public class Spawn : MonoBehaviour
{
    /// <summary>
    /// Road points container.
    /// </summary>
    [SerializeField]
    private RoadConfig roadConfig;
    /// <summary>
    /// You can spawn units only on your spawns, I guess.
    /// </summary>
    [SerializeField]
    private Alignment alignment;

    public Alignment Alignment => alignment;
    public Vector3[] GetRoad() => roadConfig.nodes;
}
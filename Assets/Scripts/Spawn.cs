using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private RoadConfig roadConfig;
    [SerializeField]
    private Alignment alignment;

    public Alignment Alignment => alignment;
    public Vector3[] GetRoad() => roadConfig.nodes;
}

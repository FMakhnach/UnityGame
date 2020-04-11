using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private RoadConfig roadConfig;

    public Vector3[] GetRoad() => roadConfig.nodes;
}

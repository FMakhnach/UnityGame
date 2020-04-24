using UnityEngine;

public abstract class PlaceArea : MonoBehaviour
{
    [SerializeField]
    private Alignment alignment;
    [SerializeField]
    private Transform rotObject;

    public Quaternion Rotation => rotObject.rotation;
    public Alignment Alignment => alignment;
}

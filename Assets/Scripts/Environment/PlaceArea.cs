using UnityEngine;

public abstract class PlaceArea : MonoBehaviour
{
    /// <summary>
    /// Well for now we have strong differentiation in placements.
    /// </summary>
    [SerializeField]
    private PlayerManager owner;
    /// <summary>
    /// The rotation the object will get on placing.
    /// </summary>
    [SerializeField]
    private Transform rotObject;

    public Quaternion Rotation => rotObject.rotation;
    public PlayerManager Owner => owner;
}

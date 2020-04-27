using UnityEngine;

public abstract class PlaceArea : MonoBehaviour
{
    [SerializeField]
    private PlayerManager owner;
    [SerializeField]
    private Transform rotObject;

    public Quaternion Rotation => rotObject.rotation;
    public PlayerManager Owner => owner;
}

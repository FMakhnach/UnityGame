using UnityEngine;

/// <summary>
/// Represents a ghost of the object.
/// </summary>
public class Ghost : MonoBehaviour
{
    /// <summary>
    /// Green ghost.
    /// </summary>
    [SerializeField]
    private GameObject good;
    /// <summary>
    /// Red ghost.
    /// </summary>
    [SerializeField]
    private GameObject bad;
    /// <summary>
    /// If the ghost is in proper place.
    /// </summary>
    private bool isFit;

    public void SetFit(bool isFit)
    {
        if (isFit != this.isFit)
        {
            this.isFit = isFit;
            bad.SetActive(!isFit);
            good.SetActive(isFit);
        }
    }

    private void Awake()
    {
        isFit = true;
        SetFit(false);
    }
}

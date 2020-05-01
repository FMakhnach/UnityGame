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
    /// Radius of the base of the ghost. Need it to check if ghost fits.
    /// </summary>
    [SerializeField]
    private float baseRadius;
    /// <summary>
    /// Layer mask on which we can place the particular object that the ghost represents.
    /// </summary>
    [SerializeField]
    private LayerMask placementMask;
    /// <summary>
    /// Layers of obstacles [Targetables].
    /// </summary>
    [SerializeField]
    private LayerMask obstaclesMask;
    /// <summary>
    /// List of points around the base circle. Actually, its X and Z coordinates.
    /// </summary>
    private Vector2[] trackingPoints;
    /// <summary>
    /// Number of tracking points.
    /// </summary>
    [SerializeField]
    private int numOfBaseTrackingPoints;
    /// <summary>
    /// Indicates if the object can be placed in this position.
    /// </summary>
    private bool isFit;
    private PlaceArea currentPlaceArea;

    /// <summary>
    /// Indicates if the object can be placed in this position.
    /// </summary>
    public bool IsFit
    {
        get => isFit;
        protected set
        {
            if (isFit != value)
            {
                isFit = value;
                // Always changing appearance of the ghost on value change.
                UpdateGhost();
            }
        }
    }
    public PlaceArea PlaceArea => currentPlaceArea;
    public PlayerManager Owner { get; set; }

    /// <summary>
    /// Checks if the object can be placed in the ghost position.
    /// </summary>
    public void CheckIfFits()
    {
        foreach (var point in trackingPoints)
        {
            var x = transform.position.x + point.x;
            var z = transform.position.z + point.y;

            if (Physics.Raycast(
                origin: new Vector3(x, transform.position.y + 0.1f, z),
                direction: Vector3.down,
                hitInfo: out RaycastHit hit,
                maxDistance: 10f,
                layerMask: placementMask))
            {
                PlaceArea area = hit.collider.GetComponent<PlaceArea>();
                // Check for 1) area existance 2) alignment 3) obstacles
                if (area != null && area.Owner == this.Owner
                    && Physics.OverlapSphere(area.transform.position, baseRadius, obstaclesMask).Length == 0)
                {
                    transform.position = area.transform.position;
                    transform.rotation = area.Rotation;
                    IsFit = true;
                    currentPlaceArea = area;
                    return;
                }
            }
        }

        IsFit = false;
    }

    private void Awake()
    {
        IsFit = false;

        if (numOfBaseTrackingPoints > 0)
        {
            trackingPoints = new Vector2[numOfBaseTrackingPoints];
            float delta = 360 / numOfBaseTrackingPoints;
            float alpha = 0f;
            // Making a circle of points around the base.
            // Calculating it there to save some time on calculating trigonometry.
            for (int i = 0; i < numOfBaseTrackingPoints; i++, alpha += delta)
            {
                trackingPoints[i]
                    = new Vector2(baseRadius * Mathf.Cos(alpha), baseRadius * Mathf.Sin(alpha));
            }
        }
        else
        {
            trackingPoints = new Vector2[0];
        }
    }
    private void UpdateGhost()
    {
        bad.SetActive(!isFit);
        good.SetActive(isFit);
    }
}

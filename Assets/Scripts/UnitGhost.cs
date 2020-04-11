using UnityEngine;

public class UnitGhost : Ghost
{
    private Spawn currentSpawn;
    public Spawn Spawn => currentSpawn;

    public override void CheckIfFits()
    {
        // Pretty much the same, but we also need to take out spawn object 
        // and do some pretty auto-fitting.
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
                Spawn spawn = hit.collider.GetComponentInParent<Spawn>();
                // Check 1) for spawn 2) for units on spawn 
                if (spawn != null && Physics.OverlapSphere(spawn.transform.position, baseRadius, obstaclesMask).Length == 0)
                {
                    transform.position = spawn.transform.position;
                    currentSpawn = spawn;
                    IsFit = true;
                    return;
                }
            }
        }

        IsFit = false;
    }
}

using UnityEngine;

public class MachineGunTower : Tower
{
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private GameObject turret;
    [SerializeField]
    private GameObject firePoint;
    private ITarget currentTarget;
    private float timer;

    private void Update()
    {
        if (currentTarget != null)
        {
            // Super fucking weird thing lol
            // There's something wrong with object destroying
            if (currentTarget.ToString() == "null")
            {
                currentTarget = null;
                return;
            }

            Aim();
            if (timer < config.attackingInterval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer -= config.attackingInterval;
                Fire(currentTarget);
            }
        }
        else
        {
            ScanTerritory();
        }
    }
    private void ScanTerritory()
    {
        float dist, minDistance = float.MaxValue;
        ITarget current;
        var colliders = Physics.OverlapSphere(transform.position, config.radius, TargetableMask);
        if (colliders != null)
        {
            foreach (var col in colliders)
            {
                current = col.gameObject.GetComponentInParent<ITarget>();
                if (current != null && current.Alignment != Alignment)
                {
                    dist = Vector3.Distance(current.TargetPoint.position, transform.position);
                    if (dist < minDistance)
                    {
                        currentTarget = current;
                        minDistance = dist;
                        timer = 0f;
                    }
                }
            }
        }
    }
    private void Aim()
    {
        var rot = turret.transform.rotation.eulerAngles;
        turret.transform.LookAt(currentTarget.TargetPoint.position);
        turret.transform.rotation = Quaternion.Euler(rot.x, turret.transform.rotation.eulerAngles.y, rot.z);
    }
    protected void Fire(ITarget target)
    {
        Bullet proj = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        var direction = target.TargetPoint.position - proj.transform.position;
        direction.y = 0;
        proj.Initialize(direction, config.damage, config.projectileSpeed, Alignment);
    }
}

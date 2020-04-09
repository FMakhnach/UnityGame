using UnityEngine;

public abstract class AttackingTower : Tower
{
    private const float maxTurretSlope = 10f;

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private Transform turret;
    [SerializeField]
    private Transform firePoint;
    private ITarget currentTarget;
    private float timer;

    private void Update()
    {
        if (currentTarget != null)
        {
            // Super fucking weird thing lol
            // There's something wrong with object destroying
            if (currentTarget.ToString() == "null"
                || Vector3.Distance(currentTarget.TargetPoint.position, transform.position) > config.radius)
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
                Fire();
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
        var colliders = Physics.OverlapSphere(transform.position, config.radius, targetableMask);
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
        turret.LookAt(currentTarget.TargetPoint.position);
        var curRot = turret.rotation.eulerAngles;
        if (curRot.x > 180f) curRot.x -= 360f;
        turret.rotation = Quaternion.Euler(Mathf.Clamp(curRot.x, -maxTurretSlope, maxTurretSlope), curRot.y, 0f);
    }
    private void Fire()
    {
        fireParticles.Play();
        audioSource.PlayOneShot(config.attackSound, 0.3f);

        Projectile proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, firePoint);
        var direction = currentTarget.TargetPoint.position - proj.transform.position;
        proj.Initialize(direction, config.damage, Alignment);
    }
}

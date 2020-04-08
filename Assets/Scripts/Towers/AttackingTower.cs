using UnityEngine;

public abstract class AttackingTower : Tower
{
    private const float maxTurretSlope = 10f;

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private GameObject turret;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private AudioClip attackSound;
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
        turret.transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = turret.transform.rotation.eulerAngles;
        turret.transform.rotation = Quaternion.Euler(Mathf.Min(curRot.x, maxTurretSlope), curRot.y, 0f);
    }
    private void Fire()
    {
        var rot = fireParticles.transform.rotation.eulerAngles;
        fireParticles.transform.LookAt(currentTarget.TargetPoint);
        fireParticles.transform.rotation = Quaternion.Euler(rot.x, turret.transform.rotation.eulerAngles.y, rot.z);

        fireParticles.Play();
        audioSource.PlayOneShot(attackSound, 0.3f);

        Projectile proj = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        var direction = currentTarget.TargetPoint.position - proj.transform.position;
        proj.Initialize(direction, config.damage, Alignment);
    }
}

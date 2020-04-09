using UnityEngine;

public class Copter : Unit
{
    private const float maxCopterSlope = 8f;

    protected override void Aim()
    {
        transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Mathf.Min(curRot.x, maxCopterSlope), curRot.y, 0f);
    }
}

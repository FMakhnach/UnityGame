using UnityEngine;

public class Buggy : Unit
{
    public const int Cost = 10;

    protected override void Aim()
    {
        transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, curRot.y, 0f);
    }
}

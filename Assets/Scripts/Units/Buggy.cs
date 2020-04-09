using UnityEngine;

public class Buggy : Unit
{
    protected override void Aim()
    {
        transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, curRot.y, 0f);
    }
}

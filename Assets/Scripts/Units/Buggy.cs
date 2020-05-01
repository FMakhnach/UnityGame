using UnityEngine;

public class Buggy : Unit
{
    public const int Cost = 20;

    protected override void Aim()
    {
        body.transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = body.transform.rotation.eulerAngles;
        body.transform.rotation = Quaternion.Euler(0f, curRot.y, 0f);
    }
    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Buggy;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}

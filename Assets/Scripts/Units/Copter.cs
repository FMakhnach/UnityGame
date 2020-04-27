using UnityEngine;

public class Copter : Unit
{
    public const int Cost = 15;
    private const float maxCopterSlope = 8f;
    [SerializeField]
    private GameObject body;

    protected override void Aim()
    {
        transform.LookAt(currentTarget.TargetPoint.position);
        var curRot = transform.rotation.eulerAngles;
        body.transform.rotation = Quaternion.Euler(Mathf.Min(curRot.x, maxCopterSlope), curRot.y, 0f);
        transform.rotation = Quaternion.Euler(0f, curRot.y, 0f);
    }

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Copter;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthText;
    }
}

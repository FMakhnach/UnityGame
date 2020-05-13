using UnityEngine;

[CreateAssetMenu(menuName = "Unit Factory")]
public class UnitFactory : ScriptableObject
{
    [SerializeField]
    private Buggy buggyPrefab;
    [SerializeField]
    private Copter copterPrefab;

    [SerializeField]
    private Material buggyMaterial;
    [SerializeField]
    private Material copterMaterial;

    public Buggy CreateBuggy(PlayerManager owner)
    {
        Buggy buggy = PoolManager.Instance.GetBuggy();
        buggy.Owner = owner;
        buggy.gameObject.GetComponentInChildren<Renderer>().material = buggyMaterial;
        return buggy;
    }
    public Copter CreateCopter(PlayerManager owner)
    {
        Copter copter = PoolManager.Instance.GetCopter();
        copter.Owner = owner;
        copter.gameObject.GetComponentInChildren<Renderer>().material = copterMaterial;
        return copter;
    }
}

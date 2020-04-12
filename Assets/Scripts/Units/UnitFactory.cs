using UnityEngine;

[CreateAssetMenu()]
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

    public Buggy CreateBuggy()
    {
        Buggy buggy = Instantiate(buggyPrefab);
        buggy.gameObject.GetComponentInChildren<Renderer>().material = buggyMaterial;
        return buggy;
    }
    public Copter CreateCopter()
    {
        Copter copter = Instantiate(copterPrefab);
        copter.gameObject.GetComponentInChildren<Renderer>().material = copterMaterial;
        return copter;
    }
}

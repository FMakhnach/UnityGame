using UnityEngine;

[CreateAssetMenu()]
public class UnitFactory : ScriptableObject
{
    [SerializeField]
    private Buggy buggyPrefab;
    [SerializeField]
    private Copter copterPrefab;

    public Buggy CreateBuggy()
    {
        Buggy buggy = Instantiate(buggyPrefab);
        return buggy;
    }
    public Copter CreateCopter()
    {
        Copter copter = Instantiate(copterPrefab);
        return copter;
    }
}

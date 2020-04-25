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

    private Vector3 spawnPosition = new Vector3(0f, 100f, 0f);

    public Buggy CreateBuggy()
    {
        Buggy buggy = Instantiate(buggyPrefab, spawnPosition, Quaternion.identity);
        buggy.gameObject.GetComponentInChildren<Renderer>().material = buggyMaterial;
        return buggy;
    }
    public Copter CreateCopter()
    {
        Copter copter = Instantiate(copterPrefab, spawnPosition, Quaternion.identity);
        copter.gameObject.GetComponentInChildren<Renderer>().material = copterMaterial;
        return copter;
    }
}

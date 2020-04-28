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

    private Vector3 spawnPosition = new Vector3(0f, 100f, 0f);

    public Buggy CreateBuggy(PlayerManager owner)
    {
        Buggy buggy = Instantiate(buggyPrefab, spawnPosition, Quaternion.identity);
        buggy.Owner = owner;
        buggy.gameObject.GetComponentInChildren<Renderer>().material = buggyMaterial;
        return buggy;
    }
    public Copter CreateCopter(PlayerManager owner)
    {
        Copter copter = Instantiate(copterPrefab, spawnPosition, Quaternion.identity);
        copter.Owner = owner;
        copter.gameObject.GetComponentInChildren<Renderer>().material = copterMaterial;
        return copter;
    }
}

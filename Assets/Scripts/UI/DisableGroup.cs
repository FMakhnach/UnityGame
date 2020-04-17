using UnityEngine;

/// <summary>
/// Represents group objects in scene that should be
/// disabled/enabled simultaneously at some point 
/// (e.g. on opening options menu).
/// </summary>
public class DisableGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToDisable;
    [SerializeField]
    private bool areEnabled;

    public void SetEnabled(bool enabled)
    {
        if (enabled != areEnabled)
        {
            foreach (var o in objectsToDisable)
            {
                o.SetActive(enabled);
            }
            areEnabled = enabled;
        }
    }
}
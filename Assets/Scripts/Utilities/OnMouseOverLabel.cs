using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverLabel : MonoBehaviour
{
    [SerializeField]
    private GameObject label;
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = EventSystem.current;
    }

    private void OnMouseEnter()
    {
        if (eventSystem.IsPointerOverGameObject())
        {
            return;
        }

        label.SetActive(true);

        var camera = Camera.main;
        label.transform.LookAt(camera.gameObject.transform);
        var rot = label.transform.rotation.eulerAngles;
        label.transform.rotation = Quaternion.Euler(
            new Vector3(rot.x, camera.transform.rotation.eulerAngles.y + 180f, rot.z));
    }
    private void OnMouseExit()
    {
        label.SetActive(false);
    }
}

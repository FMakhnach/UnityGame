using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouseOverInfoPanel : MonoBehaviour
{
    /// <summary>
    /// Fancy circle around object. Indicates that info panel should be fixed.
    /// </summary>
    [SerializeField]
    private SpriteRenderer visualization;
    /// <summary>
    /// To store current event system.
    /// </summary>
    private EventSystem eventSystem;
    /// <summary>
    /// To set correct HP.
    /// </summary>
    private DamageableBehaviour damageableBehaviour;

    /// <summary>
    /// Panel that shows stats of the object.
    /// </summary>
    [HideInInspector]
    public ObjectInfoPanel panel;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        damageableBehaviour = GetComponent<DamageableBehaviour>();
    }
    
    private void OnMouseEnter()
    {
        if (eventSystem.IsPointerOverGameObject() || ObjectInfoPanelController.Instance.PanelIsFixed)
        {
            return;
        }

        ObjectInfoPanelController.Instance.gameObject.SetActive(true);
        ObjectInfoPanelController.Instance.SetPanel(panel);
        panel.healthLabel.text = ((int)damageableBehaviour.Health).ToString();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectInfoPanelController.Instance.LockPanel(visualization);
        }
        if (!ObjectInfoPanelController.Instance.gameObject.activeSelf)
        {
            OnMouseEnter();
        }
    }
    private void OnMouseExit()
    {
        if (!ObjectInfoPanelController.Instance.PanelIsFixed)
        {
            ObjectInfoPanelController.Instance.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (ObjectInfoPanelController.Instance.ActiveVisualization == visualization 
            || panel == ObjectInfoPanelController.Instance.ActivePanel)
        {
            ObjectInfoPanelController.Instance.UnlockPanel();
        }
    }
}

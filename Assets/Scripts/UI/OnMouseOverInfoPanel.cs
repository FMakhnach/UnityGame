using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverInfoPanel : MonoBehaviour
{
    /// <summary>
    /// Fancy circle around object. Indicates that info panel should be fixed.
    /// </summary>
    [SerializeField]
    private GameObject visualization;
    /// <summary>
    /// To store current event system.
    /// </summary>
    private EventSystem eventSystem;
    /// <summary>
    /// If the panel is currently fixed.
    /// </summary>
    private static bool panelIsFixed;
    private static GameObject currentActiveVisualization;

    /// <summary>
    /// Panel that shows stats of the object.
    /// </summary>
    [HideInInspector]
    public ObjectInfoPanel panel;

    private void Awake()
    {
        eventSystem = EventSystem.current;
    }
    private void Update()
    {
        if (panelIsFixed && Input.GetMouseButtonDown(1))
        {
            panelIsFixed = false;
            ObjectInfoPanelController.Instance.gameObject.SetActive(false);
            currentActiveVisualization?.SetActive(false);
            if (currentActiveVisualization != null && currentActiveVisualization.ToString() == "null")
            {
                currentActiveVisualization = null;
            }
        }
    }
    private void OnMouseEnter()
    {
        if (eventSystem.IsPointerOverGameObject() || panelIsFixed)
        {
            return;
        }

        ObjectInfoPanelController.Instance.gameObject.SetActive(true);
        ObjectInfoPanelController.Instance.SetPanel(panel);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            panelIsFixed = true;
            if (currentActiveVisualization != null && currentActiveVisualization.ToString() != "null")
            {
                currentActiveVisualization.SetActive(false);
            }
            visualization.SetActive(true);
            currentActiveVisualization = visualization;
            ObjectInfoPanelController.Instance.SetPanel(panel);
        }
        if (!ObjectInfoPanelController.Instance.gameObject.activeSelf)
        {
            OnMouseEnter();
        }
    }
    private void OnMouseExit()
    {
        if (!panelIsFixed)
        {
            ObjectInfoPanelController.Instance.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Show some stuff when the player hovers mouse over button (turret, unit etc stats/cost).
/// </summary>
public class OnMouseOverButtonPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ObjectInfoPanel panel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ObjectInfoPanelController.Instance.PanelIsFixed)
        {
            ObjectInfoPanelController.Instance.gameObject.SetActive(true);
            ObjectInfoPanelController.Instance.SetPanel(panel);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ObjectInfoPanelController.Instance.PanelIsFixed)
        {
            ObjectInfoPanelController.Instance.gameObject.SetActive(false);
        }
    }
}

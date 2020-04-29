using UnityEngine;
using UnityEngine.EventSystems;

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

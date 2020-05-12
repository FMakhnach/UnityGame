using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulesMenu : MonoBehaviour
{
    /// <summary>
    /// Text object with gameplay description.
    /// </summary>
    [SerializeField]
    private TMP_Text gameplayText;
    /// <summary>
    /// Text object with controls description.
    /// </summary>
    [SerializeField]
    private TMP_Text controlsText;
    /// <summary>
    /// To change content form gameplay to controls and visa versa.
    /// </summary>
    [SerializeField]
    private ScrollRect scrollRect;
    /// <summary>
    /// Just to turn it on when opening rules menu.
    /// </summary>
    [SerializeField]
    private Toggle gameplayToggle;

    public void GameplayToggle(bool active)
    {
        gameplayText.gameObject.SetActive(active);
        scrollRect.content = gameplayText.GetComponent<RectTransform>();
    }
    public void ControlsToggle(bool active)
    {
        controlsText.gameObject.SetActive(active);
        scrollRect.content = controlsText.GetComponent<RectTransform>();
    }
    public void ContinueButton()
    {
        LevelLoader.Instance.LoadLevel("Level1");
    }

    private void OnEnable()
    {
        gameplayToggle.isOn = true;
    }
}

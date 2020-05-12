using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private ExitGamePopup exitGamePopup;
    [SerializeField]
    private LevelSelector levelSelector;

    public void PlayButtonClicked()
    {
        levelSelector.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OptionsButtonClicked()
    {
        OptionsMenu.Instance.disableOnOptionsOpen = this.gameObject;
        OptionsMenu.Instance.OpenOptionsMenu();
    }
    public void ExitButtonClicked()
    {
        exitGamePopup.SetActiveMenuButtons(false);
        exitGamePopup.gameObject.SetActive(true);
    }
}

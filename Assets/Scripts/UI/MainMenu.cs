using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private ExitGamePopup exitGamePopup;
    [SerializeField]
    private LevelSelector levelSelector;
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void PlayButtonClicked()
    {
        levelSelector.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Opens options window.
    /// </summary>
    public void OptionsButtonClicked()
    {
        OptionsMenu.Instance.disableOnOptionsOpen = this.gameObject;
        OptionsMenu.Instance.OpenOptionsMenu();
    }
    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitButtonClicked()
    {
        exitGamePopup.gameObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The pause menu itself.
    /// </summary>
    [SerializeField]
    private GameObject pauseMenu;
    /// <summary>
    /// What we should disable on pause menu opening.
    /// </summary>
    [SerializeField]
    private DisableGroup disableGroup;
    [SerializeField]
    private ExitToMainMenuPopup exitToMainMenuPopup;

    /// <summary>
    /// Opens options window.
    /// </summary>
    public void OptionsButtonClicked()
    {
        OptionsMenu.Instance.disableOnOptionsOpen = this.gameObject;
        OptionsMenu.Instance.OpenOptionsMenu();
    }
    /// <summary>
    /// Gets player to the main menu.
    /// </summary>
    public void MainMenuButtonClicked()
    {
        exitToMainMenuPopup.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        disableGroup.SetEnabled(true);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(transform);
        disableGroup.SetEnabled(false);
        Time.timeScale = 0f;
    }
}

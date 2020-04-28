using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The pause menu itself.
    /// </summary>
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private ExitToMainMenuPopup exitToMainMenuPopup;

    /// <summary>
    /// Should disable this on pause.
    /// </summary>
    [SerializeField]
    private GameObject gameUI;
    /// <summary>
    /// Should disable this on pause.
    /// </summary>
    [SerializeField]
    private PlayerManager playerManager;
    /// <summary>
    /// Should disable this on pause.
    /// </summary>
    [SerializeField]
    private PlayerManager enemyManager;

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
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        // Checking if the game has started. 
        if (GameManager.Instance.StartTime != default)
        {
            enemyManager.gameObject.SetActive(true);
            playerManager.gameObject.SetActive(true);
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(transform);
        gameUI.SetActive(false);
        enemyManager.gameObject.SetActive(false);
        playerManager.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
}

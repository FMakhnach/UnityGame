using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuObject;
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
    /// Should disable this on pause.
    /// </summary>
    [SerializeField]
    private TMP_Text timerText;
    private bool gameIsPaused;

    public void PauseButtonClicked()
    {
        if (gameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void TryAgainButtonClicked()
    {
        GameTimer.Instance.SetTimeScale(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OptionsButtonClicked()
    {
        OptionsMenu.Instance.disableOnOptionsOpen = this.gameObject;
        OptionsMenu.Instance.OpenOptionsMenu();
    }
    public void MainMenuButtonClicked()
    {
        exitToMainMenuPopup.gameObject.SetActive(true);
        pauseMenuObject.SetActive(false);
    }

    private void ResumeGame()
    {
        gameIsPaused = false;
        pauseMenuObject.SetActive(false);
        gameUI.SetActive(true);
        timerText.gameObject.SetActive(true);
        GameTimer.Instance.ResetTimeScale();
        // Checking if the game has started. 
        if (LevelManager.Instance.GameHasStarted)
        {
            enemyManager.gameObject.SetActive(true);
            playerManager.gameObject.SetActive(true);
        }
    }
    private void PauseGame()
    {
        gameIsPaused = true;
        pauseMenuObject.SetActive(true);
        gameUI.SetActive(false);
        timerText.gameObject.SetActive(false);
        GameTimer.Instance.PauseGame();
    }
}

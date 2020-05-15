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
        LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        GameTimer.Instance.SetTimeScale(1f);
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

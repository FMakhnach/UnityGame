using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Indicates if the game is paused.
    /// </summary>
    private bool gameIsPaused;
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

    /// <summary>
    /// Opens or closes pause menu.
    /// </summary>
    public void PauseMenuTrigger()
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        gameIsPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuTrigger();
        }
    }
    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        disableGroup.SetEnabled(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    private void PauseGame()
    {
        pauseMenu.SetActive(transform);
        disableGroup.SetEnabled(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}

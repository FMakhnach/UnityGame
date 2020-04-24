using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("Level0");
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
        Application.Quit();
    }
}

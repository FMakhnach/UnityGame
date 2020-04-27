using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenuPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

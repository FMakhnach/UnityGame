using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteWindow : MonoBehaviour
{
    public void LastLevelNextLevelButton()
    {
        gameObject.SetActive(true);
    }
    public void MainMenuButton()
    {
        GameTimer.Instance.SetTimeScale(1f);
        SceneManager.LoadScene("MainMenu");
    }
}

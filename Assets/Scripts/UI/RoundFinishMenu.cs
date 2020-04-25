using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundFinishMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void TryAgainButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

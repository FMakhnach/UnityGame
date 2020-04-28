using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundFinishMenu : MonoBehaviour
{
    [SerializeField]
    private PostGameDetailsMenu detailsMenu;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;
    private int bestScore;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
        if (score > bestScore)
        {
            bestScore = score;
        }
        bestScoreText.text = bestScore.ToString();
    }
    public void OpenDetailsMenu()
    {
        detailsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
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
    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt("best-score");
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("best-score", bestScore);
    }
}

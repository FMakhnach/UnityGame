using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundFinishMenu : MonoBehaviour
{
    [SerializeField]
    private PostGameDetailsMenu detailsMenu;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;
    [SerializeField]
    private int levelId;
    [SerializeField]
    private Button nextLevelButton;
    private int bestScore;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("best-score" + levelId, bestScore);
        }

        bestScoreText.text = bestScore.ToString();
        if (bestScore >= LevelLoader.Instance.scoreToUnlockNextLevel[levelId])
        {
            LevelLoader.Instance.UnlockLevel(levelId + 1);
            nextLevelButton.interactable = true;
        }
    }
    public void OpenDetailsMenu()
    {
        detailsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void TryAgainButton()
    {
        GameTimer.Instance.SetTimeScale(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevelButton()
    {
        GameTimer.Instance.SetTimeScale(1f);
        LevelLoader.Instance.LoadLevel(LevelLoader.Instance.levelNames[levelId + 1]);
    }
    public void MainMenuButton()
    {
        GameTimer.Instance.SetTimeScale(1f);
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt("best-score" + levelId, 0);
        nextLevelButton.interactable = false;
    }
}

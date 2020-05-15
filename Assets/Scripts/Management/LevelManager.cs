using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    #region This stuff we just enable on game start.
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private EnemyAI[] enemies;
    [SerializeField]
    private Button[] gameButtons;
    [SerializeField]
    private SpeedUpButton speedUpButton;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject timerText;
    #endregion

    /// <summary>
    /// The menu that is shown is the player loses.
    /// </summary>
    [SerializeField]
    private RoundFinishMenu loseWindow;
    /// <summary>
    /// The menu that is shown is the player loses.
    /// </summary>
    [SerializeField]
    private RoundFinishMenu winWindow;
    /// <summary>
    /// Menu with post-game stats.
    /// </summary>
    [SerializeField]
    private PostGameDetailsMenu detailsMenu;
    [SerializeField]
    private Button startButton;

    public event Action onGameStarted;

    /// <summary>
    /// Finishes the game and shows post-game menu.
    /// </summary>
    public void EndGame(PlayerManager winner, PlayerManager loser)
    {
        if (winner == player)
        {
            loser.gameObject.SetActive(false);
            if (FindObjectOfType<EnemyAI>() == null)
            {
                Invoke("PlayerWon", 1f);
            }
        }
        else
        {
            Invoke("PlayerLost", 1f);
        }
    }
    public void StartGame()
    {
        SetActivePreGameGroup(true);
        startButton.gameObject.SetActive(false);
        onGameStarted?.Invoke();
    }

    private void Start()
    {
        SetActivePreGameGroup(false);
    }
    /// <summary>
    /// Calculates match score of the player based on some stats.
    /// </summary>
    private int CalculateScore(PlayerManager.Stats stats)
    {
        int score = 20000
            + 20 * stats.UnitsKilled
            + 30 * stats.TurretsKilled
            - 10 * stats.UnitsLost
            - 15 * stats.TurretsLost
            - stats.MoneySpent * 5
            - (int)GameTimer.Instance.GameTime * 5;
        return score > 0 ? score : 100;
    }
    private void SetActivePreGameGroup(bool active)
    {
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].enabled = active;
            gameButtons[i].GetComponent<KeyboardButton>().enabled = active;
        }
        speedUpButton.gameObject.SetActive(active);
        timerText.SetActive(active);
    }
    private void PlayerWon()
    {
        Time.timeScale = 0f;
        gameUI.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        winWindow.gameObject.SetActive(true);
        winWindow.SetScore(CalculateScore(player.PlayerStats));
        detailsMenu.Initialize(player.PlayerStats, winWindow);
    }
    private void PlayerLost()
    {
        Time.timeScale = 0f;
        gameUI.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        loseWindow.gameObject.SetActive(true);
        loseWindow.SetScore(0);
        detailsMenu.Initialize(player.PlayerStats, loseWindow);
    }
}

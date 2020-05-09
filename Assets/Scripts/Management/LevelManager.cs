using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    #region This stuff we just enable on game start.
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private PlayerManager enemy;
    [SerializeField]
    private Button[] gameButtons;
    [SerializeField]
    private SpeedUpButton speedUpButton;
    #endregion

    /// <summary>
    /// This thing we turn off on game end (similar to pause menu).
    /// </summary>
    [SerializeField]
    private DisableGroup disableGroup;
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

    public bool GameHasStarted => !startButton.gameObject.activeSelf;
    public event Action onGameStarted;

    /// <summary>
    /// Finishes the game and shows post-game menu.
    /// </summary>
    public void EndGame(PlayerManager winner, PlayerManager loser)
    {
        Time.timeScale = 0f;
        disableGroup.SetEnabled(false);
        if (winner == player)
        {
            winWindow.gameObject.SetActive(true);
            winWindow.SetScore(CalculateScore(player.PlayerStats));
            detailsMenu.Initialize(player.PlayerStats, winWindow);
        }
        else
        {
            loseWindow.gameObject.SetActive(true);
            loseWindow.SetScore(0);
            detailsMenu.Initialize(player.PlayerStats, loseWindow);
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
        Invoke("DisablePreGameGroup", 0.01f);
    }
    /// <summary>
    /// Calculates match score of the player based on some stats.
    /// </summary>
    private int CalculateScore(PlayerManager.Stats stats)
    {
        int score = 1000
            + 50 * stats.UnitsKilled
            + 75 * stats.TurretsKilled
            - 20 * stats.UnitsLost
            - 30 * stats.TurretsLost
            - stats.MoneySpent
            - (int)GameTimer.Instance.GameTime;
        return score;
    }
    private void DisablePreGameGroup() => SetActivePreGameGroup(false);
    private void SetActivePreGameGroup(bool active)
    {
        player.gameObject.SetActive(active);
        enemy.gameObject.SetActive(active);
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].enabled = active;
            gameButtons[i].GetComponent<KeyboardButton>().enabled = active;
        }
        GameTimer.Instance.gameObject.SetActive(active);
        speedUpButton.gameObject.SetActive(active);
    }
}

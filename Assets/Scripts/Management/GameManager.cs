using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private PlayerManager enemy;
    [SerializeField]
    private DisableGroup disableGroup;
    [SerializeField]
    private RoundFinishMenu loseWindow;
    [SerializeField]
    private RoundFinishMenu winWindow;
    [SerializeField]
    private PostGameDetailsMenu detailsMenu;
    [SerializeField]
    private Button[] gameButtons;
    [SerializeField]
    private Button startButton;
    private DateTime startTime;
    public DateTime StartTime => startTime;

    public void EndGame(PlayerManager winner, PlayerManager loser)
    {
        Time.timeScale = 0f;
        disableGroup.SetEnabled(false);
        int timeSpent = (int)(DateTime.Now - startTime).TotalSeconds;
        if (winner == player)
        {
            winWindow.gameObject.SetActive(true);
            winWindow.SetScore(CalculateScore(player.Stat, timeSpent));
            detailsMenu.Initialize(player.Stat, timeSpent, winWindow);
        }
        else
        {
            loseWindow.gameObject.SetActive(true);
            loseWindow.SetScore(0);
            detailsMenu.Initialize(player.Stat, timeSpent, loseWindow);
        }

    }
    public void StartGame()
    {
        player.gameObject.SetActive(true);
        enemy.gameObject.SetActive(true);
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].enabled = true;
        }
        startButton.gameObject.SetActive(false);
        startTime = DateTime.Now;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].enabled = false;
        }
    }
    private int CalculateScore(PlayerManager.Stats stats, int timeSpent)
    {
        int score = 500
            + 50 * stats.UnitsKilled
            + 75 * stats.TurretsKilled
            - 20 * stats.UnitsLost
            - 30 * stats.TurretsLost
            - stats.MoneySpent
            - timeSpent;
        return score;
    }
}

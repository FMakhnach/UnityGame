using System;
using TMPro;
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
    private Button[] gameButtons;
    [SerializeField]
    private SpeedUpButton speedUpButton;

    [SerializeField]
    private DisableGroup disableGroup;
    [SerializeField]
    private RoundFinishMenu loseWindow;
    [SerializeField]
    private RoundFinishMenu winWindow;
    [SerializeField]
    private PostGameDetailsMenu detailsMenu;


    [SerializeField]
    private Button startButton;

    public bool GameHasStarted => !startButton.gameObject.activeSelf;

    public void EndGame(PlayerManager winner, PlayerManager loser)
    {
        Time.timeScale = 0f;
        disableGroup.SetEnabled(false);
        if (winner == player)
        {
            winWindow.gameObject.SetActive(true);
            winWindow.SetScore(CalculateScore(player.Stat));
            detailsMenu.Initialize(player.Stat, winWindow);
        }
        else
        {
            loseWindow.gameObject.SetActive(true);
            loseWindow.SetScore(0);
            detailsMenu.Initialize(player.Stat, loseWindow);
        }

    }
    public void StartGame()
    {
        SetActivePreGameGroup(true);
        startButton.gameObject.SetActive(false);
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
        SetActivePreGameGroup(false);
    }
    private int CalculateScore(PlayerManager.Stats stats)
    {
        int score = 500
            + 50 * stats.UnitsKilled
            + 75 * stats.TurretsKilled
            - 20 * stats.UnitsLost
            - 30 * stats.TurretsLost
            - stats.MoneySpent
            - (int)GameTimer.Instance.GameTime;
        return score;
    }
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

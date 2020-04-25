using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private DisableGroup disableGroup;
    [SerializeField]
    private RoundFinishMenu loseWindow;
    [SerializeField]
    private RoundFinishMenu winWindow;

    public void LoseGame(PlayerManager player)
    {
        Time.timeScale = 0f;
        disableGroup.SetEnabled(false);
        if (player == this.player)
        {
            loseWindow.gameObject.SetActive(true);
            loseWindow.SetScore(0);
        }
        else
        {
            winWindow.gameObject.SetActive(true);
            winWindow.SetScore(0);
        }
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
}

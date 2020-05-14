using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Window with post game stats.
/// </summary>
public class PostGameDetailsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeSpent;
    [SerializeField]
    private TMP_Text energySpent;
    [SerializeField]
    private TMP_Text unitsKilled;
    [SerializeField]
    private TMP_Text turretsKilled;
    [SerializeField]
    private TMP_Text unitsLost;
    [SerializeField]
    private TMP_Text turretsLost;
    [SerializeField]
    private Button backButton;
    /// <summary>
    /// Should open it on "Back" button click.
    /// </summary>
    private RoundFinishMenu menu;

    public void Initialize(PlayerManager.Stats stats, RoundFinishMenu menu)
    {
        timeSpent.text = GameTimer.Instance.GetTimeString();
        energySpent.text = stats.MoneySpent.ToString();
        unitsKilled.text = stats.UnitsKilled.ToString();
        turretsKilled.text = stats.TurretsKilled.ToString();
        unitsLost.text = stats.UnitsLost.ToString();
        turretsLost.text = stats.TurretsLost.ToString();
        this.menu = menu;

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackButton);
    }

    private void BackButton()
    {
        menu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Button[] levelButtons;

    public void LoadLevel(string levelName)
    {
        LevelLoader.Instance.LoadLevel(levelName);
    }
    public void BackButton()
    {
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        int buttonsToEnable = Mathf.Min(LevelLoader.Instance.LevelsUnlocked, levelButtons.Length);
        for (int i = 0; i < buttonsToEnable; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}

using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private LevelLoader levelLoader;

    public void LoadLevel(string levelName)
    {
        levelLoader.gameObject.SetActive(true);
        levelLoader.LoadLevel(levelName);
    }
    public void BackButton()
    {
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

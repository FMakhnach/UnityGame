using UnityEngine;
using UnityEngine.UI;

public class ExitGamePopup : MonoBehaviour
{
    [SerializeField]
    private Button[] menuButtons;

    public void SetActiveMenuButtons(bool active)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(active);
        }
    }
    public void BackButton()
    {
        SetActiveMenuButtons(true);
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        Application.Quit();
    }
}

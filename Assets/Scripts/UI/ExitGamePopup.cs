using UnityEngine;

public class ExitGamePopup : MonoBehaviour
{
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
    public void ConfirmButton()
    {
        Application.Quit();
    }
}

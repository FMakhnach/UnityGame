 using UnityEngine;

public class LanguageChoice : MonoBehaviour
{
    public void ChangeLanguage(int value)
    {
        LanguageManager.Instance.SetLanguage((Language)value);
    }
}

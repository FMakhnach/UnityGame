using UnityEngine;

public class LanguageSetting : MonoBehaviour
{
    [SerializeField]
    private Language language;

    private void OnEnable()
    {
        LanguageManager.Instance.SetLanguage(language);
    }
}

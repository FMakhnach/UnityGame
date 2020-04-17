using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MultiLanguageLabel : MonoBehaviour
{
    private TMP_Text text;
    private string engText;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        engText = text.text;
    }
    private void Start()
    {
        TranslateText();
        LanguageManager.Instance.onLanguageChanged += TranslateText;
    }
    public void TranslateText()
    {
        text.text = LanguageManager.Instance.Translate(engText);
    }
    private void OnDestroy()
    {
        LanguageManager.Instance.onLanguageChanged -= TranslateText;
    }
}

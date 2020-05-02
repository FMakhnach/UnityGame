using TMPro;
using UnityEngine;

public class MultiLanguageLabel : MonoBehaviour
{
    private TMP_Text text;

    [SerializeField]
    [TextArea(5, 20)]
    private string englishText;
    [SerializeField]
    [TextArea(5, 20)]
    private string russianText;

    public void TranslateText()
    {
        switch (LanguageManager.Instance.CurrentLanguage)
        {
            case Language.English:
                text.text = englishText;
                break;
            case Language.Russian:
                text.text = russianText;
                break;
            default:
                text.text = "LANGUAGE NOT FOUND";
                break;
        }
    }

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        TranslateText();
        LanguageManager.Instance.onLanguageChanged += TranslateText;
    }
    private void OnDestroy()
    {
        LanguageManager.Instance.onLanguageChanged -= TranslateText;
    }
}

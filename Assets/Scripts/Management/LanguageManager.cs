using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Singleton for managing language switching.
/// </summary>
public class LanguageManager : MonoBehaviour
{
    private static LanguageManager instance;
    public static LanguageManager Instance => instance;

    /// <summary>
    /// First key is language, second key is text in eng.
    /// </summary>
    private Dictionary<Language, Dictionary<string, string>> vocabulary;
    [SerializeField]
    /// <summary>
    /// Asset that keeps labels translations.
    /// </summary>
    private LanguageVocabulary vocabularySource;

    private Language currentLanguage;
    public Language CurrentLanguage => currentLanguage;

    /// <summary>
    /// All labels should subscribe it to change language in game.
    /// </summary>
    public event Action onLanguageChanged;

    /// <summary>
    /// Gets the text translation, if the vocabulary contains it.
    /// Returns "MISSING TEXT" if not.
    /// </summary>
    public string Translate(string text)
    {
        if (vocabulary[currentLanguage].ContainsKey(text))
        {
            return vocabulary[currentLanguage][text];
        }
        else
        {
            return "MISSING TEXT";
        }
    }
    public void SetLanguage(Language newLanguage)
    {
        if (newLanguage == currentLanguage)
        {
            return;
        }
        else
        {
            currentLanguage = newLanguage;
            onLanguageChanged?.Invoke();
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
        DontDestroyOnLoad(this.gameObject);

        // Inititalizing vocabulary from the asset.
        vocabulary = new Dictionary<Language, Dictionary<string, string>>();
        vocabulary[Language.English] = new Dictionary<string, string>();
        vocabulary[Language.Russian] = new Dictionary<string, string>();
        for (int i = 0; i < vocabularySource.englishValues.Length; i++)
        {
            string key = vocabularySource.englishValues[i];
            vocabulary[Language.English][key] = vocabularySource.englishValues[i];
            vocabulary[Language.Russian][key] = vocabularySource.russianValues[i];
        }

        currentLanguage = (Language)PlayerPrefs.GetInt("language");
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("language", (int)CurrentLanguage);
    }
}

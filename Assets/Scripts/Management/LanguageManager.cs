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

    private Language currentLanguage;
    public Language CurrentLanguage => currentLanguage;

    /// <summary>
    /// All labels should subscribe it to change language in game.
    /// </summary>
    public event Action onLanguageChanged;

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

        currentLanguage = (Language)PlayerPrefs.GetInt("language");
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("language", (int)CurrentLanguage);
    }
}

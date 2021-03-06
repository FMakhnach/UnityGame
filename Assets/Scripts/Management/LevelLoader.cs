﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text percent;
    private int levelsUnlocked;
    public string[] levelNames;
    public int[] scoreToUnlockNextLevel;

    public int LevelsUnlocked => levelsUnlocked;

    public void UnlockLevel(int id)
    {
        if (id >= levelsUnlocked && id < levelNames.Length)
        {
            levelsUnlocked++;
            PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
        }
    }
    /// <summary>
    /// Loads level with fancy loading screen.
    /// </summary>
    public void LoadLevel(string levelName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelName));
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
    }
    /// <summary>
    /// Loads level with fancy loading screen.
    /// </summary>
    private IEnumerator LoadLevelAsync(string levelName)
    {
        // Waiting for click sound.
        yield return new WaitForSeconds(0.45f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        while (!operation.isDone)
        {
            slider.value = operation.progress * 1.1111112f; // <=> / 0.9
            percent.text = ((int)(slider.value * 100)).ToString();
            yield return null;
        }
        FindObjectOfType<PoolManager>().Initialize();
        gameObject.SetActive(false);
    }
}

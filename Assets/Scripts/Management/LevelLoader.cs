using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text percent;

    /// <summary>
    /// Loads level with fancy loading screen.
    /// </summary>
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAsync(levelName));
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
        gameObject.SetActive(false);
    }
}

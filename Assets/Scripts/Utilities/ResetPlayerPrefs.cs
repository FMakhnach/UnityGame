using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("soundsVolume", 0.5f);
        PlayerPrefs.SetFloat("musicVolume", 0.5f);
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        PlayerPrefs.SetInt("window-mode", 0);
        PlayerPrefs.SetInt("best-score1", 0);
        PlayerPrefs.SetInt("best-score2", 0);
        PlayerPrefs.SetInt("best-score3", 0);
        PlayerPrefs.SetInt("language", 1);
    }
}

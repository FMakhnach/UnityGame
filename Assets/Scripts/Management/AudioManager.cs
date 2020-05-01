using System;
using UnityEngine;

/// <summary>
/// Singleton for managing audio.
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    /// <summary>
    /// Audio source for theme music (child).
    /// </summary>
    [SerializeField]
    private AudioSource musicSource;
    /// <summary>
    /// Audio source for UI sfx (child).
    /// </summary>
    [SerializeField]
    private AudioSource uiEffectsSource;
    /// <summary>
    /// Theme music clip.
    /// </summary>
    [SerializeField]
    private AudioClip musicClip;
    /// <summary>
    /// Sound that plays on pressing buttons.
    /// </summary>
    [SerializeField]
    private AudioClip buttonClickSound;
    /// <summary>
    /// Current master volume.
    /// </summary>
    private float currentMasterVolume;

    /// <summary>
    /// Current master volume.
    /// </summary>
    public float MasterVolume => currentMasterVolume;

    /// <summary>
    /// Triggers on changing master volume.
    /// </summary>
    public event Action<float> masterVolumeChanged;

    public void SetMasterVolume(float volume)
    {
        currentMasterVolume = volume;
        masterVolumeChanged?.Invoke(volume);
    }
    /// <summary>
    /// Subscribe this method to button onClick event to get click sound.
    /// </summary>
    public void ButtonClicked()
    {
        uiEffectsSource.PlayOneShot(buttonClickSound);
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

        masterVolumeChanged += (float vol) => musicSource.volume = vol;
        masterVolumeChanged += (float vol) => uiEffectsSource.volume = vol;

        currentMasterVolume = PlayerPrefs.GetFloat("masterVolume");
    }
    private void Start()
    {
        // Set up music
        musicSource.clip = musicClip;
        musicSource.Play();
        // Set volume from loaded
        SetMasterVolume(currentMasterVolume);
    }
    private void OnApplicationQuit()
    {
        // Save master volume
        PlayerPrefs.SetFloat("masterVolume", currentMasterVolume);
    }
}
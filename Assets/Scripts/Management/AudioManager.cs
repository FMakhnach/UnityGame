using System;
using UnityEngine;

/// <summary>
/// Singleton for managing audio.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    /// <summary>
    /// Audio source for theme music (child).
    /// </summary>
    [SerializeField]
    private AudioSource musicSource;
    /// <summary>
    /// Audio source for UI sfx (child).
    /// </summary>
    [SerializeField]
    private AudioSource soundsSource;
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
    private float soundsVolume;

    public float SoundsVolume => soundsVolume;
    public float MusicVolume => musicSource.volume;

    /// <summary>
    /// Triggers on changing sounds volume.
    /// </summary>
    public event Action<float> soundsVolumeChanged;

    public void SetSoundsVolume(float volume)
    {
        soundsVolume = volume;
        soundsVolumeChanged?.Invoke(volume);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    /// <summary>
    /// Subscribe this method to button onClick event to get click sound.
    /// </summary>
    public void ButtonClicked()
    {
        soundsSource.PlayOneShot(buttonClickSound);
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        soundsVolumeChanged += (float vol) => soundsSource.volume = vol;

        soundsVolume = PlayerPrefs.GetFloat("soundsVolume", 1f);
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1f);
    }
    private void Start()
    {
        // Set up music
        musicSource.clip = musicClip;
        musicSource.Play();
        // Set volume from loaded
        SetSoundsVolume(soundsVolume);
    }
    private void OnApplicationQuit()
    {
        // Save volume
        PlayerPrefs.SetFloat("soundsVolume", soundsVolume);
        PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
    }
}
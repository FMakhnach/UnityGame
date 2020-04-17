using UnityEngine;

/// <summary>
/// Simple class for audio sources to change with master volume.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GameEffectsAudioSource : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = AudioManager.Instance.MasterVolume;
        AudioManager.Instance.masterVolumeChanged += SetVolume;
    }
    private void SetVolume(float volume)
    {
        source.volume = volume;
    }
    private void OnDestroy()
    {
        AudioManager.Instance.masterVolumeChanged -= SetVolume;
    }
}
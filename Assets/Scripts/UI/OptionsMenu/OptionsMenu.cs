using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton that represents options menu.
/// </summary>
public class OptionsMenu : Singleton<OptionsMenu>
{
    /// <summary>
    /// To change it on game loading.
    /// </summary>
    [SerializeField]
    private Slider masterVolumeSlider;
    /// <summary>
    /// We have a common options menu through scenes, so we should 
    /// disable different objects on opening it in different scenes.
    /// </summary>
    [HideInInspector]
    public GameObject disableOnOptionsOpen;

    /// <summary>
    /// Closes the options menu returns to the previous one.
    /// </summary>
    public void BackButtonClicked()
    {
        gameObject.SetActive(false);
        disableOnOptionsOpen.SetActive(true);
    }
    /// <summary>
    /// Opens the options menu.
    /// </summary>
    public void OpenOptionsMenu()
    {
        gameObject.SetActive(true);
        disableOnOptionsOpen.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Sets slider and language dropdown values to loaded and turns the menu off.
    /// </summary>
    private void Start()
    {
        // Setting slider in proper position
        masterVolumeSlider.value = AudioManager.Instance.MasterVolume;
        // We don't want it on scene loading
        gameObject.SetActive(false);
    }
}
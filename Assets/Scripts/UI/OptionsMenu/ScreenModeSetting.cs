using UnityEngine;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField]
    private FullScreenMode fullscreenMode;

    private void OnEnable()
    {
        Screen.fullScreenMode = fullscreenMode;
    }
}

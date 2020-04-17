using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple class that adds sound to button click.
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(AudioManager.Instance.ButtonClicked);
    }
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple class for a button that should be activated by keyboard.
/// </summary>
public class KeyboardButton : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private KeyCode keyCode;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            button.onClick?.Invoke();
        }
    }
}

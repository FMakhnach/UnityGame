using UnityEngine;

public class DropdownMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject dropdownBackground;
    private bool isActive;

    private void Awake()
    {
        dropdownBackground.SetActive(true);
        isActive = true;
    }

    public void OpenOrClose()
    {
        isActive = !isActive;
        dropdownBackground.SetActive(isActive);
    }
}

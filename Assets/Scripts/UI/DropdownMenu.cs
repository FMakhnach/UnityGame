using UnityEngine;

public class DropdownMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform dropdownBackground;
    private bool isActive;

    private void Awake()
    {
        //dropdownBackground.gameObject.SetActive(false);
        //isActive = false;
        dropdownBackground.gameObject.SetActive(true);
        isActive = true;
    }

    public void OpenOrClose()
    {
        isActive = !isActive;
        dropdownBackground.gameObject.SetActive(isActive);
    }
}

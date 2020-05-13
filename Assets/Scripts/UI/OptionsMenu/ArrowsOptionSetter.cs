using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a multichoice setting that can be changed via arrows from left and right.
/// Idk really how it is called.
/// </summary>
public class ArrowsOptionSetter : MonoBehaviour
{
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button right;
    /// <summary>
    /// Setting name in PlayerPrefs (to save and load it).
    /// Actually, we're saving and loading id in array, which is essential.
    /// </summary>
    [SerializeField]
    private string playerPrefsName;
    protected int id;

    public TMP_Text[] options;

    public void LeftArrowClicked()
    {
        options[id].gameObject.SetActive(false);
        id--;
        if (id == -1)
        {
            id = options.Length - 1;
        }
        options[id].gameObject.SetActive(true);
    }
    public void RightArrowClicked()
    {
        options[id].gameObject.SetActive(false);
        id++;
        if (id == options.Length)
        {
            id = 0;
        }
        options[id].gameObject.SetActive(true);
    }

    private void Awake()
    {
        left.onClick.AddListener(LeftArrowClicked);
        right.onClick.AddListener(RightArrowClicked);
    }
    private void Start()
    {
        id = PlayerPrefs.GetInt(playerPrefsName, 0);
        options[id].gameObject.SetActive(true);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(playerPrefsName, id);
    }
}

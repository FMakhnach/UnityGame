using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a multichoice setting that can be changed via arrows from left and right.
/// </summary>
public class ArrowsOptionSetter : MonoBehaviour
{
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button right;
    [SerializeField]
    private string playerPrefsName;
    protected int id;
    protected int defaultId;

    public TMP_Text[] options;

    protected virtual void Awake()
    {
        left.onClick.AddListener(LeftArrowClicked);
        right.onClick.AddListener(RightArrowClicked);
        defaultId = 0;
    }
    protected virtual void Start()
    {
        id = PlayerPrefs.GetInt(playerPrefsName, defaultId);
        options[id].gameObject.SetActive(true);
    }
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
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(playerPrefsName, id);
    }
}

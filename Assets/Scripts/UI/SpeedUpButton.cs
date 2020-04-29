using TMPro;
using UnityEngine;

public class SpeedUpButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private float[] speedUpOptions;
    private int currentSpeedId;

    private void Awake()
    {
        currentSpeedId = 0;
    }
    public void SpeedUpButtonClicked()
    {
        if(++currentSpeedId == speedUpOptions.Length)
        {
            currentSpeedId = 0;
        }
        text.text = "x" + speedUpOptions[currentSpeedId].ToString("#.#");
        GameTimer.Instance.SetTimeScale(speedUpOptions[currentSpeedId]);
    }
}

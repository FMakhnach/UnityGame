using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GameTimer : MonoBehaviour
{
    private static GameTimer instance;
    public static GameTimer Instance => instance;

    private TMP_Text text;
    private float time;
    private float timeScale;

    public float GameTime => time;

    public void SetTimeScale(float scale)
    {
        if (scale <= 0f)
        {
            print("Scale {scale} is not supported. Use PauseGame().");
            return;
        }
        timeScale = scale;
        Time.timeScale = scale;
    }
    public void ResetTimeScale()
    {
        Time.timeScale = timeScale;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public string GetTimeString()
    {
        int time = (int)this.time;
        int minutes = time / 60;
        int seconds = time % 60;
        return (minutes < 10 ? "0" : "") + minutes + ":" +
               (seconds < 10 ? "0" : "") + seconds;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        time = 0f;
        timeScale = 1f;
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        text.text = GetTimeString();
    }
}

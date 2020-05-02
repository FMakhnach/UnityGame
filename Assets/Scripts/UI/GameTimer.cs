using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{
    [SerializeField]
    private TMP_Text text;
    private float time;
    private float timeScale;
    /// <summary>
    /// Amound of real seconds in which game second passes. 
    /// So its like 0.5 for 2 and etc.
    /// </summary>
    private float scaledSecond;
    /// <summary>
    /// Need it to slow down on time scale.
    /// </summary>
    private CameraMovement mainCamera;

    public float GameTime => time;

    public void SetTimeScale(float scale)
    {
        if (scale <= 0f)
        {
            print($"Scale {scale} is not supported. Use PauseGame().");
            return;
        }
        timeScale = scale;
        Time.timeScale = scale;
        scaledSecond = 1 / scale;
        mainCamera.ScaleSpeed(scaledSecond);
    }
    public void ResetTimeScale()
    {
        Time.timeScale = timeScale;
        scaledSecond = 1 / timeScale;
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

    protected override void Awake()
    {
        base.Awake();
        time = 0f;
        timeScale = 1f;
        scaledSecond = 1f;
        mainCamera = Camera.main.transform.parent.GetComponent<CameraMovement>();
    }
    private void Start()
    {
        LevelManager.Instance.onGameStarted += () => StartCoroutine("Tick");
    }
    /// <summary>
    /// Ticks every scaled second
    /// </summary>
    /// <returns></returns>
    private IEnumerator Tick()
    {
        for (; ; )
        {
            if (Time.timeScale == 0f)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(scaledSecond);
                time += scaledSecond;
                text.text = GetTimeString();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject towers;
    [SerializeField]
    private GameObject units;
    [SerializeField]
    private GameObject buildings;
    private int currentActive;
    private GameObject[] bars;

    public void RightButtonClicked()
    {
        bars[currentActive].SetActive(false);
        if (currentActive == bars.Length - 1)
        {
            currentActive = 0;
        }
        else
        {
            currentActive++;
        }
        bars[currentActive].SetActive(true);
    }
    public void LeftButtonClicked()
    {
        bars[currentActive].SetActive(false);
        if (currentActive == 0)
        {
            currentActive = bars.Length - 1;
        }
        else
        {
            currentActive--;
        }
        bars[currentActive].SetActive(true);
    }

    private void Awake()
    {
        bars = new GameObject[] { towers, units, buildings };
        currentActive = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoPanelController : MonoBehaviour
{
    private static ObjectInfoPanelController instance;
    public static ObjectInfoPanelController Instance => instance;

    [SerializeField]
    private UnitInfoPanel buggyPanel;
    [SerializeField]
    private UnitInfoPanel copterPanel;
    [SerializeField]
    private TowerInfoPanel laserPanel;
    [SerializeField]
    private TowerInfoPanel mgPanel;
    [SerializeField]
    private PlantInfoPanel plantPanel;

    public UnitInfoPanel Buggy => buggyPanel;
    public UnitInfoPanel Copter => copterPanel;
    public TowerInfoPanel Laser => laserPanel;
    public TowerInfoPanel MG => mgPanel;
    public PlantInfoPanel Plant => plantPanel;


    private ObjectInfoPanel currentActive;

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
        gameObject.SetActive(false);
    }

    public void SetPanel(ObjectInfoPanel panel)
    {
        if(currentActive != null)
        {
            currentActive.gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(true);
        currentActive = panel;
    }
}

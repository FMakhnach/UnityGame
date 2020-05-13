using UnityEngine;

public class ObjectInfoPanelController : Singleton<ObjectInfoPanelController>
{
    [SerializeField]
    private UnitInfoPanel buggyPanel;
    [SerializeField]
    private UnitInfoPanel copterPanel;
    [SerializeField]
    private TurretInfoPanel laserPanel;
    [SerializeField]
    private TurretInfoPanel mgPanel;
    [SerializeField]
    private PlantInfoPanel plantPanel;

    public UnitInfoPanel Buggy => buggyPanel;
    public UnitInfoPanel Copter => copterPanel;
    public TurretInfoPanel Laser => laserPanel;
    public TurretInfoPanel MG => mgPanel;
    public PlantInfoPanel Plant => plantPanel;

    public bool PanelIsFixed { get; private set; }
    public SpriteRenderer ActiveVisualization { get; private set; }
    public ObjectInfoPanel ActivePanel { get; private set; }
    public DamageableBehaviour Target { get; set; }

    public void SetPanel(ObjectInfoPanel panel)
    {
        if (ActivePanel != null)
        {
            ActivePanel.gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(true);
        ActivePanel = panel;
    }
    public void LockPanel(SpriteRenderer visualization)
    {
        PanelIsFixed = true;
        RemoveCurrentVisualization();
        SetVisualization(visualization);
    }
    public void UnlockPanel()
    {
        PanelIsFixed = false;
        RemoveCurrentVisualization();
        if (Instance != null)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetVisualization(SpriteRenderer visualization)
    {
        ActiveVisualization = visualization;
        ActiveVisualization.gameObject.SetActive(true);
    }
    private void RemoveCurrentVisualization()
    {
        if (ActiveVisualization != null)
        {
            ActiveVisualization.gameObject.SetActive(false);
            ActiveVisualization = null;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PanelIsFixed && Input.GetMouseButtonDown(1))
        {
            UnlockPanel();
        }
    }
}

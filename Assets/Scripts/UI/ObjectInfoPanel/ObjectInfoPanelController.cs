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

    private bool panelIsFixed;
    /// <summary>
    /// A circle around the selected object.
    /// </summary>
    private SpriteRenderer activeVisualization;
    /// <summary>
    /// The gui thing on the left side of the screen.
    /// </summary>
    private ObjectInfoPanel activePanel;

    public bool PanelIsFixed => panelIsFixed;
    public SpriteRenderer ActiveVisualization => activeVisualization;
    public ObjectInfoPanel ActivePanel => activePanel;

    public void SetPanel(ObjectInfoPanel panel)
    {
        if (activePanel != null)
        {
            activePanel.gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(true);
        activePanel = panel;
    }
    public void LockPanel(SpriteRenderer visualization)
    {
        panelIsFixed = true;
        RemoveCurrentVisualization();
        SetVisualization(visualization);
    }
    public void UnlockPanel()
    {
        panelIsFixed = false;
        RemoveCurrentVisualization();
        if (Instance != null)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetVisualization(SpriteRenderer visualization)
    {
        activeVisualization = visualization;
        activeVisualization.gameObject.SetActive(true);
    }
    private void RemoveCurrentVisualization()
    {
        if (activeVisualization != null)
        {
            activeVisualization.gameObject.SetActive(false);
            activeVisualization = null;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (panelIsFixed && Input.GetMouseButtonDown(1))
        {
            UnlockPanel();
        }
    }
}

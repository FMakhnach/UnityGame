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
    private SpriteRenderer currentActiveVisualization;
    private ObjectInfoPanel currentActivePanel;

    public bool PanelIsFixed => panelIsFixed;
    public SpriteRenderer ActiveVisualization => currentActiveVisualization;
    public ObjectInfoPanel ActivePanel => currentActivePanel;

    public void SetPanel(ObjectInfoPanel panel)
    {
        if (currentActivePanel != null)
        {
            currentActivePanel.gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(true);
        currentActivePanel = panel;
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
        currentActiveVisualization = visualization;
        currentActiveVisualization.gameObject.SetActive(true);
    }
    private void RemoveCurrentVisualization()
    {
        if (currentActiveVisualization != null)
        {
            currentActiveVisualization.gameObject.SetActive(false);
            currentActiveVisualization = null;
        }
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

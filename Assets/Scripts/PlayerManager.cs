using TMPro;
using UnityEngine;

/// <summary>
/// Manages players units. 
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private UnitFactory unitFactory;
    [SerializeField]
    private TowerFactory towerFactory;
    [SerializeField]
    private Alignment alignment;
    [SerializeField]
    private TextMeshProUGUI currencyText;
    private int startingCurrency = 100;
    private int currency;

    public int Currency
    {
        get => currency;
        private set
        {
            Debug.Assert(value <= currency, "Trying to put negative to currency!");
            currency = value;
            UpdateCurrencyText();
        }
    }

    private void Awake()
    {
        currency = startingCurrency;
        UpdateCurrencyText();
    }

    public void SpawnBuggy(SpawnTile spawn)
    {
        unitFactory.CreateBuggy().SpawnOn(spawn, Alignment.Computer);
    }
    public void SpawnCopter(SpawnTile spawn)
    {
        unitFactory.CreateCopter().SpawnOn(spawn, Alignment.Computer);
    }
    public void PlaceLaserTower(TowerTile tile)
    {
        towerFactory.CreateLaserTower().SpawnOn(tile, alignment);
        Currency -= LaserTower.Cost;
    }
    public void PlaceMGTower(TowerTile tile)
    {
        towerFactory.CreateMGTower().SpawnOn(tile, alignment);
        Currency -= MachineGunTower.Cost;
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = Currency.ToString();
    }
}

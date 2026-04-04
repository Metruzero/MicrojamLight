using System.Data;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private int StartingCurrency;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameManager gameManager;

    private int currency;
    private float fuel;
    private float maxFuel = 100;

    public void Start()
    {
        fuel = maxFuel;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void AdjustCurrency(int adjustment)
    {
        currency += adjustment;
    }

    public void ReduceFuel(float adjustment)
    {
        fuel -= adjustment;
        uiManager.UpdateFuel(fuel / maxFuel);
    }
}

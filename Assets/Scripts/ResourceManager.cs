using System;
using System.Collections.Generic;
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

    private Dictionary<ItemType, int> itemCounts;

    private int currency;
    private float fuel;
    private float maxFuel = 100;

    public void Start()
    {
        itemCounts = new Dictionary<ItemType, int>();
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            itemCounts.Add(type, 0);
        }
        fuel = maxFuel;
        currency = StartingCurrency;
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
        fuel = Mathf.Clamp(fuel, 0, 100);
        uiManager.UpdateFuel(fuel / maxFuel);
    }

    public void AddItem(ItemType type)
    {
        itemCounts[type]++;
        Debug.Log(type + " " + itemCounts[type]);
    }
}

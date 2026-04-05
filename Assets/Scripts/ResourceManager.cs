using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceManager : MonoBehaviour
{
    public List<ItemDataSO> datas;

    [SerializeField]
    private int StartingCurrency;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameManager gameManager;

    private Dictionary<ItemType, ItemDataSO> items;

    private Dictionary<ItemType, int> itemCounts;

    private int currency;
    private float fuel;
    private float maxFuel = 100;

    private InputAction switchAction;
    private InputAction useAction;

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("Switch");
        useAction = InputSystem.actions.FindAction("Use");
    }

    private void Update()
    {
        
    }

    public void Start()
    {
        items = new Dictionary<ItemType, ItemDataSO>();
        foreach (ItemDataSO item in datas)
        {
            items.Add(item.type, item);
        }


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
        uiManager.UpdateCurrency(currency);
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
        uiManager.UpdateUIWithItems(itemCounts);
    }

    public void SellItem(ItemType type)
    {
        if (itemCounts[type] > 0)
        {
            itemCounts[type]--;
            currency += items[type].cost;
        }

        uiManager.UpdateUIWithItems(itemCounts);
    }

    public void PushUpdateToUI()
    {
        uiManager.UpdateUIWithItems(itemCounts);
    }
}

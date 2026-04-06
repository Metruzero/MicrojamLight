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
    private GameState gameState;

    private int selectedItem;

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("Switch");
        useAction = InputSystem.actions.FindAction("Use");
        selectedItem = 0;
    }

    private void Update()
    {
        if (gameState == GameState.Active)
        {
            if (switchAction.WasPressedThisFrame())
            {
                Debug.Log(selectedItem);
                SwitchSelectedItem();
                uiManager.UpdateIndicator((ItemType)selectedItem);
            }

            if (useAction.WasPressedThisFrame())
            {
                UseSelectedItem();
            }
        }
    }

    private void SwitchSelectedItem()
    {
        selectedItem++;
        if (selectedItem >= 3)
        {
            selectedItem = 0;
        }
    }

    private void UseSelectedItem()
    {
        ItemType type = (ItemType)selectedItem;
        if (itemCounts[type] > 0)
        {
            itemCounts[type]--;
            fuel += GetSelectedItemFuel();
        }
        uiManager.UpdateUIWithItems(itemCounts);
    }

    private float GetSelectedItemFuel()
    {
        ItemType type = (ItemType)selectedItem;
        return items[type].fuelValue;
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
        uiManager.UpdateIndicator((ItemType)selectedItem);
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
        uiManager.UpdateFuel((fuel / maxFuel), (Mathf.Clamp(fuel + GetSelectedItemFuel(), 0, 100) / maxFuel));
        if (fuel < 0.01f)
        {
            gameManager.TriggerGameOver();
        }

    }

    public void AddItem(ItemType type)
    {
        itemCounts[type]++;
        uiManager.UpdateUIWithItems(itemCounts);
    }

    public void SellItem(int itemInt)
    {
        ItemType type = (ItemType)itemInt;
        if (itemCounts[type] > 0)
        {
            itemCounts[type]--;
            AdjustCurrency(items[type].cost);
        }

        uiManager.UpdateUIWithItems(itemCounts);
    }

    public void PushUpdateToUI()
    {
        uiManager.UpdateUIWithItems(itemCounts);
    }

    public void UpdateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public void HardReset()
    {
        currency = StartingCurrency;
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public class UpgradeButton
{
    public GameObject buttonObject;
    public TextMeshProUGUI upgradeTitleTMP;
    public TextMeshProUGUI upgradeDescriptionTMP;
    public TextMeshProUGUI upgradeCostTMP;
}
public class UIManager : MonoBehaviour
{
    public List<ItemDataSO> datas;

    [SerializeField]
    private TextMeshProUGUI timeTMP;

    [SerializeField]
    private GameObject fuelBarParent, fuelBar;

    [SerializeField]
    private GameObject gameplayPanel, shopPanel;

    [SerializeField]
    private List<UpgradeButton> upgradeButtons;

    private RectTransform fuelBarRect;

    [SerializeField]
    private GameObject dialogueContainerPanel, upgradePanel, sellPanel, levelCompletePanel;

    [SerializeField]
    private TextMeshProUGUI gameplayBarrelTMP, gameplayBottleTMP, gameplayBatteryTMP;

    [SerializeField]
    private TextMeshProUGUI shopBarrelTMP, shopBottleTMP, shopBatteryTMP;

    [SerializeField]
    private TextMeshProUGUI currencyTMP;

    [SerializeField]
    private TextMeshProUGUI sellBarrelTMP, sellBottleTMP, sellBatteryTMP;

    [SerializeField]
    private Button sellBarrelButton, sellBottleButton, sellBatteryButton;

    private Dictionary<ItemType, ItemDataSO> items;

    [SerializeField]
    private RectTransform barrelIndicator, batteryIndicator, bottleIndicator;

    public GameObject ArrowIndicator;

    private void Awake()
    {
        fuelBarRect = fuelBar.GetComponent<RectTransform>();
        items = new Dictionary<ItemType, ItemDataSO>();
        foreach (ItemDataSO item in datas)
        {
            items.Add(item.type, item);
        }
    }

    public void UpdateTime(float time)
    {
        timeTMP.text = time.ToString("#");
    }

    public void UpdateFuel(float fuelPercentage)
    {
        fuelBarRect.localScale = new Vector3(1f, fuelPercentage, 1f);
    }

    public void UpdateUpgradeButton(string upgradeTitle, string upgradeDescription, int cost, bool enable, int index)
    {
        UpgradeButton currentButton = upgradeButtons[index];
        currentButton.upgradeTitleTMP.text = upgradeTitle;
        currentButton.upgradeDescriptionTMP.text = upgradeDescription;
        currentButton.upgradeCostTMP.text = cost.ToString("#");

        currentButton.buttonObject.SetActive(enable);


    }

    public void ResetUpgradeButtons()
    {
        foreach (var button in upgradeButtons)
        {
            button.buttonObject.SetActive(false);
            button.buttonObject.GetComponent<Button>().interactable = true;
        }
    }

    public void UpgradeCompleteDisableButton(int ind)
    {
        upgradeButtons[ind].buttonObject.GetComponent<Button>().interactable = false;
    }

    public void HideLeftShopBoxes()
    {
        dialogueContainerPanel.SetActive(false);
        upgradePanel.SetActive(false);
        sellPanel.SetActive(false);
    }

    public void ShowUpgradeBox()
    {
        upgradePanel.SetActive(true);
    }

    public void ShowSellBox()
    {
        sellPanel.SetActive(true);
    }

    public void ShowDialogueBox()
    {
        dialogueContainerPanel.SetActive(true);
    }

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }

    public void HideLevelCompletePanel()
    {
        levelCompletePanel.SetActive(false);
    }

    public void ShowShop()
    {
        gameplayPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void HideShop()
    {
        gameplayPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void UpdateUIWithItems(Dictionary<ItemType, int> itemCounts)
    {
        foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            switch(item)
            {
                case ItemType.OilBottle:
                    gameplayBottleTMP.text = itemCounts[item].ToString();
                    shopBottleTMP.text += itemCounts[item].ToString();
                    sellBottleTMP.text = string.Concat("Sell for ", items[item].cost.ToString());
                    sellBottleButton.interactable = itemCounts[item] > 0;
                    break;
                case ItemType.OilBarrel:
                    gameplayBarrelTMP.text = itemCounts[item].ToString();
                    shopBarrelTMP.text += itemCounts[item].ToString();
                    sellBarrelTMP.text = string.Concat("Sell for ", items[item].cost.ToString());
                    sellBarrelButton.interactable = itemCounts[item] > 0;
                    break;
                case ItemType.Battery:
                    gameplayBatteryTMP.text = itemCounts[item].ToString();
                    shopBatteryTMP.text += itemCounts[item].ToString();
                    sellBatteryTMP.text = string.Concat("Sell for ", items[item].cost.ToString());
                    sellBatteryButton.interactable = itemCounts[item] > 0;
                    break;
                default:
                    break;

            }
        }
    }

    public void UpdateCurrency(int currency)
    {
        currencyTMP.text = currency.ToString();
    }

    public void UpdateIndicator(ItemType type)
    {
        if (type == ItemType.OilBarrel)
        {
            ArrowIndicator.transform.position = barrelIndicator.transform.position;
        }
        if (type == ItemType.Battery)
        {
            ArrowIndicator.transform.position = batteryIndicator.transform.position;
        }
        if (type == ItemType.OilBottle)
        {
            ArrowIndicator.transform.position = bottleIndicator.transform.position;
        }
    }
}

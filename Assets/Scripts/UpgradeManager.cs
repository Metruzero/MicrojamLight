using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public enum UpgradeType
{
    MovementSpeed,
    LightRadius,
    FuelEfficiency,
    FasterInteraction,
    LargerLightEfficiency,
    MoreLoot,
}

[Serializable]
public class UpgradeDetails
{
    public UpgradeType type;
    public string title;
    public string description;
    public int currentLevel;
    public int maxLevel;
    public float value;
    public int cost;
    public int costScale;
}

public class UpgradeManager : MonoBehaviour
{
    // Upgrade details
    public List<UpgradeDetails> details;
    public Dictionary<UpgradeType, UpgradeDetails> upgradeDictionary;
    private List<UpgradeType> availableUpgradeTypes;
    

    [SerializeField]
    private ResourceManager resourceManager;

    [SerializeField]
    private StageManager stageManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private Player player;

    private UpgradeType[] currentUpgradeToPurchase;

    void Start()
    {
        availableUpgradeTypes = new List<UpgradeType>();
        foreach (UpgradeType upgradeType in Enum.GetValues(typeof(UpgradeType)))
        {
            availableUpgradeTypes.Add(upgradeType);
        }

        currentUpgradeToPurchase = new UpgradeType[3];

        upgradeDictionary = new Dictionary<UpgradeType, UpgradeDetails>();

        foreach (var uDetails in details)
        {
            upgradeDictionary.Add(uDetails.type, uDetails);
        }
    }

    public void RefreshUpgrades()
    {
        uiManager.ResetUpgradeButtons();
        List<UpgradeType> typesToChoose = new List<UpgradeType>();
        int amountToChoose = availableUpgradeTypes.Count < 3 ? availableUpgradeTypes.Count : 3;
        Debug.Log("Avail: " + availableUpgradeTypes.Count);

        for(int i = 0; i < amountToChoose; i++)
        {
            int ind = Random.Range(0, availableUpgradeTypes.Count);
            Debug.Log(ind);

            UpgradeType chosenType = availableUpgradeTypes[ind];

            typesToChoose.Add(chosenType);
            availableUpgradeTypes.Remove(chosenType);
            currentUpgradeToPurchase[i] = chosenType;
        }

        for (int i = 0; i < typesToChoose.Count; i++)
        {
            UpgradeType upgradeType = typesToChoose[i];
            availableUpgradeTypes.Add(upgradeType);

            UpgradeDetails uDetails = upgradeDictionary[upgradeType];

            int cost = CalculateCost(uDetails);

            uiManager.UpdateUpgradeButton(uDetails.title, uDetails.description, cost, true, i);
        }
    }

    public void PurchaseUpgrade(int index)
    {
        UpgradeType type = currentUpgradeToPurchase[index];
        Debug.Log(type);

        UpgradeDetails uDetails = upgradeDictionary[type];

        int cost = CalculateCost(uDetails);

        // Check if player has resource.
        if (cost <= resourceManager.GetCurrency())
        {
            resourceManager.AdjustCurrency(-cost);
            uDetails.currentLevel++;
            // If this raises the upgrade to max level, remove it from the pool
            if (uDetails.currentLevel >= uDetails.maxLevel)
            {
                availableUpgradeTypes.Remove(type);
            }
            uiManager.UpgradeCompleteDisableButton(index);
            PushUpgrades();
        }
    }

    private int CalculateCost(UpgradeDetails uDetails)
    {
        return uDetails.cost + (uDetails.costScale * uDetails.currentLevel);
    }

    private void PushUpgrades()
    {
        player.UpdateUpgrades(details);
        stageManager.UpdateUpgrades(upgradeDictionary);
    }

    public void HardReset()
    {
        foreach (var uDetails in upgradeDictionary)
        {
            uDetails.Value.currentLevel = 0;
        }
    }


}




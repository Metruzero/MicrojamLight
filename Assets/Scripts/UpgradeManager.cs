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
    public float costScale;
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
    private UIManager uiManager;

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

            uiManager.UpdateUpgradeButton(uDetails.title, uDetails.description, uDetails.cost, true, i);
        }
    }

    public void PurchaseUpgrade(int index)
    {

    }
}




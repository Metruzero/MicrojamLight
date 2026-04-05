using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    ResourceManager resourceManager;

    public List<ItemDataSO> datas;
    public int[] itemWeights;
    public Pickup pickupPrefab;
    public Interactable cratePrefab;

    public int[] crateItemCounts;
    public int[] createItemCountWeights;

    public List<Transform> spawnPoints;

    public int pickupCount;
    public int boxCount;

    public int interactionLevel;
    public float interactionUpgradeValue;
    public int lootLevel;
    public float lootLevelUpgradeValue;
    private int totalWeight;

    public float baseWorkDuration;

    public void Start()
    {
        totalWeight = CalculateTotalWeight();
    }

    public void GenerateStage()
    {
        if (pickupCount + boxCount > spawnPoints.Count)
        {
            throw new System.ArithmeticException("Stage cannot spawn more items than there are spawn points.");
        }

        List<Transform> availableSpawns = new List<Transform>(spawnPoints);
        int totalWeight = CalculateTotalWeight();

        for (int i = 0; i < pickupCount; i++)
        {
            int randIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randIndex];
            availableSpawns.RemoveAt(randIndex);

            ItemDataSO itemData = RandomItem();

            

            Pickup newPickup = Instantiate(pickupPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            newPickup.item.Type = itemData.type;
            newPickup.GetComponent<SpriteRenderer>().sprite = itemData.icon;



        }

        float workDuration = baseWorkDuration - (interactionLevel * interactionUpgradeValue);

        for (int i = 0; i < boxCount; i++)
        {
            int randIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randIndex];
            availableSpawns.RemoveAt(randIndex);

            int itemCount = NumberOfItemInCrates();
            Interactable newCrate = Instantiate<Interactable>(cratePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            for (int j = 0; j < itemCount; j++)
            {
                newCrate.contents.Add(RandomItem().type);
            }
            newCrate.resourceManager = this.resourceManager;
            newCrate.WorkDuration = workDuration;


        }




    }

    public ItemDataSO RandomItem()
    {
        int randomWeight = Random.Range(0, totalWeight);
        int currentSum = 0;
        ItemDataSO itemData = null;
        for (int j = 0; j < datas.Count; j++)
        {
            currentSum += itemWeights[j];
            if (randomWeight <= currentSum)
            {
                Debug.Log(j);
                itemData = datas[j];
                return itemData;
            }
        }
        return null;
    }

    public int CalculateTotalWeight()
    {
        int totalWeight = 0;
        foreach (int i in itemWeights)
        {
            totalWeight += i;
        }
        return totalWeight;
    }

    private int CalculateTotalWeightItemCounts()
    {
        int totalWeight = 0;
        for (int i = 0; i < createItemCountWeights.Length; i++)
        {
            totalWeight += createItemCountWeights[i];
        }
        return totalWeight;
    }

    public void UpdateUpgrades(Dictionary<UpgradeType, UpgradeDetails> upgradeDictionary)
    {
        lootLevel = upgradeDictionary[UpgradeType.MoreLoot].currentLevel;
        lootLevelUpgradeValue = upgradeDictionary[UpgradeType.MoreLoot].value;

        interactionLevel = upgradeDictionary[UpgradeType.FasterInteraction].currentLevel;
        interactionUpgradeValue = upgradeDictionary[UpgradeType.FasterInteraction].value;
    }

    private int NumberOfItemInCrates()
    {
        int totalWeight = CalculateTotalWeightItemCounts();
        int randomWeight = Random.Range(0, totalWeight);
        int currentSum = 0;
        int itemCount = 0;
        for (int j = 0; j < datas.Count; j++)
        {
            currentSum += createItemCountWeights[j];
            if (randomWeight < currentSum)
            {
                itemCount = crateItemCounts[j];
                return itemCount;
            }
        }
        return itemCount;
    }
}

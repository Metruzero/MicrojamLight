using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField]
    private ResourceManager resourceManager;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}




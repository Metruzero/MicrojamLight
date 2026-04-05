using System;
using UnityEngine;

public enum ItemType
{
    Battery,
    Matches,
    Oil
}

[Serializable]
public class Item
{
    public ItemType Type;
    public float fuelValue;
}

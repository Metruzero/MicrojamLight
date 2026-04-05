using System;
using UnityEngine;

public enum ItemType
{
    Battery,
    OilBottle,
    OilBarrel
}

[Serializable]
public class Item
{
    public ItemType Type;
}

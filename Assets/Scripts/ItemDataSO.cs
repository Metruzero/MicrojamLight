using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Scriptable Objects/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int cost;
    public float fuelValue;
}

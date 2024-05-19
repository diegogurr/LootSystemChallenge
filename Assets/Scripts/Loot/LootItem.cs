using UnityEngine;

public enum ItemType
{
    Weapon,
    Coin,
    Shield
}

[CreateAssetMenu(fileName = "New LootItem", menuName = "Inventory/LootItem")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject modelPrefab;

    public int power;
    public int amount;
    public int defense;

    [Range(0f, 1f)]
    public float dropChance;
}

using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    [SerializeField]
    private List<LootItem> lootItems;
    [SerializeField]
    private int poolSize = 10;
    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>();
        InitializePool();
    }

    private void InitializePool()
    {
        GameObject emptyParent = new GameObject("Loot Pool Objects");

        foreach (var lootItem in lootItems)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject lootObject = Instantiate(lootItem.modelPrefab);
                lootObject.transform.SetParent(emptyParent.transform);
                lootObject.SetActive(false);
                lootObject.name = lootItem.itemName;
                pool.Add(lootObject);
            }
        }
    }

    public GameObject GetLootObject(LootItem item)
    {
        foreach (var lootObject in pool)
        {
            if (!lootObject.activeInHierarchy && lootObject.name == item.itemName)
            {
                return lootObject;
            }
        }

        GameObject newLootObject = Instantiate(item.modelPrefab);
        newLootObject.name = item.itemName;
        newLootObject.SetActive(false);
        pool.Add(newLootObject);
        return newLootObject;
    }
}

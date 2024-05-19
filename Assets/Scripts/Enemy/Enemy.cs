using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<LootItem> possibleLoot = new List<LootItem>();
    [SerializeField]
    private LootPool lootPool;
    [SerializeField]
    private int maxLootDrops = 3;
    [SerializeField]
    private float lootSpawnRadius = 2f;
    [SerializeField]
    private GameObject vfxPrefab;

    public void Die()
    {
        SpawnLoot();
        PlayDeathVFX();
        AudioController.Instance.Play("Explosion");
        gameObject.SetActive(false);
    }

    private void SpawnLoot()
    {
        int lootDrops = Random.Range(1, maxLootDrops + 1);
        for (int i = 0; i < lootDrops; i++)
        {
            foreach (var lootItem in possibleLoot)
            {
                if (Random.value <= lootItem.dropChance)
                {
                    GameObject lootObject = lootPool.GetLootObject(lootItem);
                    Vector3 randomPosition = GetRandomPosition();
                    lootObject.transform.position = randomPosition;
                    lootObject.SetActive(true);
                    Loot lootComponent = lootObject.GetComponent<Loot>();
                    if (lootComponent != null)
                    {
                        lootComponent.SetLootItem(lootItem);
                    }
                }
            }
        }
    }

    private void PlayDeathVFX()
    {
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
            Destroy(vfxInstance, 3f);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * lootSpawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);
        return randomPosition + transform.position;
    }

    private void OnMouseDown()
    {
        Die();
    }
}

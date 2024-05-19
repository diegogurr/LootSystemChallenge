using UnityEngine;

public class Loot : MonoBehaviour
{
    private LootItem lootItem;
    [SerializeField]
    private GameObject lootVFXPrefab;

    public void SetLootItem(LootItem newItem)
    {
        lootItem = newItem;
        gameObject.name = newItem.itemName; 
    }

    private void OnMouseDown()
    {
        Collect();
    }

    private void Collect()
    {
        Debug.Log("Collected: " + lootItem.itemName);
        UIController.Instance.UpdateText("Collected: " + lootItem.itemName);

        PlayLootVFX();
        AudioController.Instance.Play("Collect");

        gameObject.SetActive(false);
    }

    private void PlayLootVFX()
    {
        if (lootVFXPrefab != null)
        {
            GameObject vfx = Instantiate(lootVFXPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
            Destroy(vfx, ps.main.duration);
        }
    }
}

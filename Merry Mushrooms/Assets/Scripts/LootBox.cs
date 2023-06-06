using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public List<Loot> lootList;

    void Start()
    {
    }

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleLoot = new List<Loot>();

        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleLoot.Add(item);
            }
        }
        if (possibleLoot.Count > 0)
        {
            Loot droppedItem = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedItem;
        }
        Debug.Log("No Loot Dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPos)
    {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem.lootPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            InstantiateLoot(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
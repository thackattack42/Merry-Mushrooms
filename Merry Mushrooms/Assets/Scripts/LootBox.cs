using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class LootBox : MonoBehaviour
{
    [CreateAssetMenu]

    public class Loot : ScriptableObject
    {
        public GameObject LootPrefab;
        public List<Loot> lootList = new List<Loot>();
        public Sprite lootSprite;
        public string lootName;
        public int dropChance;

        void Start()
        {
            lootList.Add(new Loot("FunGil", 100));
        }

        public Loot(string lootName, int dropChance)
        {
            this.lootName = lootName;
            this.dropChance = dropChance;
        }

        Loot GetLootBox()
        {
            int randomNumber = Random.Range(1, 101);
            List<Loot> possibleLoot = new List<Loot>();

            foreach(Loot item in lootList)
            {
                if (randomNumber <= item.dropChance)
                {
                    possibleLoot.Add(item);
                    return item;
                }
            }
            if (possibleLoot.Count > 0)
            {
                Loot loot = possibleLoot[Random.Range(0, possibleLoot.Count)];
                return loot;
            }
            Debug.Log("No Loot Dropped");
            return null;
        }

        public void InstantiateLoot(Vector3 spawnPos)
        {
            Loot possibleLoot = GetLootBox();
            if (possibleLoot != null)
            {
                GameObject LootGameObject = Instantiate(LootPrefab, spawnPos, Quaternion.identity);
                LootGameObject.GetComponent<SpriteRenderer>().sprite = possibleLoot.lootSprite;
            }
        }
    }  
}